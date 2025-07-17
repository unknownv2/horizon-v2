using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace XContent
{
    public class PEC
    {
        public static ulong SyncInfoRecordId = 0x0000000002;
        public static ulong IndexRecordId = 0x0000000001;

        public EndianIO IO;
        public XeConsoleSignature Signature;
        public StfsVolumeDescriptor VolumeDescriptor;
        public StfsDevice StfsPec;

        private byte[] Creator;
        public byte ConsoleIdEntryCount;
        public XProfileConsoleId[] ConsoleList;

        public PEC() { }
        public PEC(EndianIO IO)
        {
            this.IO = IO;
            if (!this.IO.Opened)
                this.IO.Open();

            this.Read();
        }

        public void Create(StfsDevice Device, ulong ProfileId)
        {
            byte[] Creator = BitConverter.GetBytes(ProfileId);
            Array.Reverse(Creator);

            this.ConsoleList = new XProfileConsoleId[1];

            this.ConsoleIdEntryCount = 0x01;

            this.SetCreator(Creator);

            this.VolumeDescriptor = new StfsVolumeDescriptor();

            MemoryStream ms = new MemoryStream();

            this.IO = new EndianIO(ms, EndianType.BigEndian, true);

            this.IO.Stream.SetLength(0x1000);

            StfsCreatePacket CreatePacket = new StfsCreatePacket();

            CreatePacket.BlockCacheElementCount = 0x05;
            CreatePacket.BackingFileOffset = 0x1000;
            CreatePacket.BackingMaximumVolumeSize = 0x00000000fffff000;
            CreatePacket.DeviceCharacteristics = 0x01;
            CreatePacket.VolumeDescriptor = this.VolumeDescriptor;

            this.StfsPec = new StfsDevice(this.IO);

            this.StfsPec.StfsCreateDevice(CreatePacket);

            this.Flush();

            this.Save();

            Device.CreateFileFromArray("PEC", ms.ToArray());

        }

        private void Read()
        {
            this.Signature = new XeConsoleSignature(this.IO);

            byte[] TopLevelHash = this.IO.In.ReadBytes(0x014);

            this.IO.In.BaseStream.Position += 0x8;

            this.VolumeDescriptor = new StfsVolumeDescriptor(this.IO);

            this.IO.In.BaseStream.Position += 4;

            this.Creator = this.IO.In.ReadBytes(8);

            this.ReadConsoleIdTable();

            StfsCreatePacket CreatePacket = new StfsCreatePacket();            

            CreatePacket.BlockCacheElementCount = 0x05;
            CreatePacket.BackingFileOffset = 0x1000;
            CreatePacket.BackingMaximumVolumeSize = 0x00000000fffff000;
            CreatePacket.DeviceCharacteristics = 0x01;
            CreatePacket.VolumeDescriptor = this.VolumeDescriptor;

            this.StfsPec = new StfsDevice(this.IO);

            this.StfsPec.StfsCreateDevice(CreatePacket);

            this.StfsPec.Read();

        }
        public void SetCreator(byte[] Creator)
        {
            if (Creator.Length == 0x08)
            {
                this.Creator = Creator;
            }
        }
        private void AddSigningId()
        {
            //Keyvault kv = new Keyvault(new EndianIO(new FileStream(@"C:\CON RSA\my_dev_kv.bin", FileMode.Open), EndianType.BigEndian));

            //kv.LoadSigningParameters();

            XeConsoleCertificate Cert = new XeConsoleCertificate(Horizon.Server.Config.getPublicKey());

            //XeConsoleCertificate Cert = new XeConsoleCertificate(kv.ConsoleCertificate);
            AddConsoleId(Cert.ConsoleId, true);

            //kv.IO.Close();
        }
        private void ReadConsoleIdTable()
        {
            this.ConsoleIdEntryCount = this.IO.In.ReadByte();
            if (this.ConsoleIdEntryCount > 0x64)
                throw new Exception("Invalid number of consoles found.");

            this.ConsoleList = new XProfileConsoleId[0x64];
            for (int x = 0; x < this.ConsoleIdEntryCount; x++)
            {
                this.ConsoleList[x].Id = this.IO.In.ReadBytes(5);
            }
        }
        public void AddConsoleId(byte[] ConsoleId, bool SigningId)
        {
            if (SigningId) // if it is the signing id, set the last consoleID to reflect the cert
            {
                this.ConsoleList[this.ConsoleIdEntryCount - 1].Id = ConsoleId;
            }
            else
            {
                bool IsConsoleIdInList = false;
                for (int x = 0; x < ConsoleIdEntryCount; x++)
                {
                    if (HorizonCrypt.ArrayEquals(this.ConsoleList[x].Id, ConsoleId))
                        IsConsoleIdInList = true;
                }
                if (!IsConsoleIdInList)
                {
                    this.ConsoleList[this.ConsoleIdEntryCount].Id = ConsoleId;

                    this.ConsoleIdEntryCount++;
                }
            }
        }

        public void Flush()
        {
            this.StfsPec.StfsControlDevice(StfsControlCode.StfsFlushDirtyBuffers, null);
            this.StfsPec.StfsControlDevice(StfsControlCode.StfsBuildVolumeDescriptor, this.VolumeDescriptor);
            this.StfsPec.StfsControlDevice(StfsControlCode.StfsResetWriteState, null);
        }
        public void Save()
        {
            this.AddSigningId(); // this is added so validation of the PEC passes

            this.IO.Out.SeekTo(0x274);
            this.IO.Out.Write(this.ConsoleIdEntryCount);

            for (int x = 0; x < this.ConsoleIdEntryCount; x++)
            {
                this.IO.Out.Write(this.ConsoleList[x].Id);
            }

            this.IO.Out.SeekTo(0x244);
            this.IO.Out.Write(this.VolumeDescriptor.ToArray());

            this.IO.Out.BaseStream.Position += 0x04;
            this.IO.Out.Write(this.Creator);

            this.IO.In.SeekTo(0x23c);

            byte[] DataToHash = this.IO.In.ReadBytes(0xDC4);
            byte[] Hash = HorizonCrypt.XeCryptSha(DataToHash,  null, null);

            this.IO.Out.SeekTo(0x228);
            this.IO.Out.Write(Hash);

            this.ConsolePrivateKeySign();
        }
        public void ConsolePrivateKeySign()
        {
            //Keyvault kv = new Keyvault(new EndianIO(new FileStream(@"C:\CON RSA\my_dev_kv.bin", FileMode.Open), EndianType.BigEndian));

            //kv.LoadSigningParameters();

            this.IO.Out.SeekTo(0x00);

            this.IO.Out.Write(Horizon.Server.Config.getPublicKey());
            //this.IO.Out.Write(kv.ConsoleCertificate);

            this.IO.In.SeekTo(0x23c);

            byte[] Sig = HorizonCrypt.FormatSignature(Horizon.Server.Config.getCONKeys(), this.IO.In.ReadBytes(0xDC4));

            //byte[] Sig = HorizonCrypt.FormatSignature(kv.SigningParams, this.IO.In.ReadBytes(this.VolumeExtension.FormatHashSize));

            this.IO.Out.SeekTo(0x1a8);
            this.IO.Out.Write(Sig);

            //kv.IO.Close();
        }
    }
}
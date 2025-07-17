using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ICE;
using Checksum;

namespace Horizon.PackageEditors.Left_4_Dead_2
{
    public partial class Left4Dead2 : EditorControl
    {
        private bool[] FilesLoaded = new bool[2];
        private IceKey Ice;

        internal static string EncryptionKey = "d7f84mDe";

        //public static readonly string FID = "454108D4";
        public Left4Dead2()
        {
            InitializeComponent();
            TitleID = FormID.Left4Dead2;
            
        }

        public override bool Entry()
        {
            string[] Filenames = new string[] { "left4dead2_cfg_pri.cfg", "left4dead2_cfg_ss.cfg" };
            FilesLoaded[0] = this.DoesFileExist(Filenames[0]);
            FilesLoaded[1] = this.DoesFileExist(Filenames[1]);
            string[] FileItems = new string[2];
            if (FilesLoaded[0] || FilesLoaded[1])
            {
                if (FilesLoaded[0])
                    this.comboBoxEx1.Items.Add(Filenames[0]);
                if (FilesLoaded[1])
                    this.comboBoxEx1.Items.Add(Filenames[1]);

                this.Ice = new IceKey(0);
                this.Ice.set(System.Text.Encoding.ASCII.GetBytes(EncryptionKey));

                this.comboBoxEx1.SelectedIndex = new int();

                return true;
            }

            return false;
        }
        public override void Save()
        {
            if (this.IO != null)
            {
                int FileSize = this.richTextBox1.Text.Length;
                while (!((FileSize % 8) == 0x00))
                    FileSize++;

                byte[] stringData = new byte[FileSize];

                var writer = new StreamWriter(new MemoryStream(stringData), Encoding.ASCII);
                writer.Write(this.richTextBox1.Text);
                writer.Close();

                this.IO.Stream.SetLength(FileSize + 4);
                this.IO.Out.SeekTo(4);
                var er = new EndianReader(new MemoryStream(stringData), EndianType.BigEndian);

                for (var x = 0; x < FileSize; x += 8)
                {
                    byte[] ds = er.ReadBytes(8);
                    this.Ice.encrypt(ds, ds);
                    this.IO.Out.Write(ds);
                }

                this.IO.In.SeekTo(4);
                var data = this.IO.In.ReadBytes(FileSize);

                this.IO.Out.SeekTo(0);
                this.IO.Out.Write(CRC32.Calculate(data));
            }
        }
        private void DecryptCfg(string Filename)
        {
            if (this.OpenStfsFile(Filename))
            {
                int FileSize = (int)this.IO.Stream.Length - 4;

                this.IO.In.SeekTo(0);
                uint set_crc = this.IO.In.ReadUInt32();

                var data = this.IO.In.ReadBytes(FileSize);

                if (set_crc != CRC32.Calculate(data))
                {
                    throw new Exception(string.Format("Left 4Dead 2: config {0} is corrupted.", Filename)); 
                }

                var ioW = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                var reader = new EndianReader(data, EndianType.BigEndian);

                for (int x = new int(); x < FileSize; x += 8)
                {
                    byte[] r_data = reader.ReadBytes(8);
                    this.Ice.decrypt(r_data, r_data);
                    ioW.Out.Write(r_data);
                }

                this.richTextBox1.Text = new StreamReader(new MemoryStream(ioW.ToArray()), Encoding.ASCII).ReadToEnd();

                ioW.Close();
            }
        }
        private void NewCfgSelected(object sender, EventArgs e)
        {
            this.DecryptCfg(this.comboBoxEx1.Text);
        }
    }
}

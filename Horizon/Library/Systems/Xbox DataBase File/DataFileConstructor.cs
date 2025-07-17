using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XboxDataBaseFile
{
    public class DataFileConstructor :IDisposable
    {
        private DataFile DataFile;
        private DataFile MemoryDataFile;
        private uint DataStart;
        private uint MaxEntries;
        public DataFileConstructor(DataFile dataFile, uint NewMaxEntries)
        {
            this.DataFile = dataFile;
            MemoryStream newDataFileStream = new MemoryStream();
            this.MemoryDataFile = new DataFile(new EndianIO(dataFile.CopyToArray(), EndianType.BigEndian));
            this.DataStart = ((dataFile.MaxFreeEntries + 3) << 3) + (NewMaxEntries * 0x12);
            this.MaxEntries = NewMaxEntries;
        }
        public DataFile Initiate()
        {
            MemoryDataFile.Read();
            //this.DataFile.DataStartOffset = this.DataStart;
            DataFile df = new DataFile();
            df.IO = new EndianIO(new MemoryStream(), EndianType.BigEndian);
            df.IO.Open();
            df.IO.Out.Write(new byte[this.DataStart]);
            df.DataStartOffset = this.DataStart;
            df.FreeStartOffset = 0x18 + (MaxEntries * 0x12);
            df.MaxEntries = MaxEntries;
            df.MaxFreeEntries = this.DataFile.MaxFreeEntries;
            df.FreeRecords.Add(new FreeRecord()
            {
                Offset = (uint)(df.IO.Stream.Length - this.DataStart),
                Size = ~(int)(this.DataStart)
            });

            for (var x = 0; x < MemoryDataFile.EntryCount; x++)
            {
                df.Upsert(MemoryDataFile.DataFileRecords[x].Id, MemoryDataFile.ReadRecord(MemoryDataFile.DataFileRecords[x].Id));
            }

            return df;
        }

        public static DataFile Create(uint EntryCount)
        {
            DataFile df = new DataFile();
            df.IO = new EndianIO(new MemoryStream(), EndianType.BigEndian);
            df.IO.Open();
            df.FreeStartOffset = 0x18 + (EntryCount * 0x12);
            df.MaxEntries = EntryCount;
            df.MaxFreeEntries = EntryCount;
            df.DataStartOffset = ((EntryCount + 3) << 3) + (EntryCount * 0x12);
            df.IO.Out.Write(new byte[df.DataStartOffset]);
            df.FreeRecords.Add(new FreeRecord()
            {
                Offset = (uint)(df.IO.Stream.Length - df.DataStartOffset),
                Size = ~(int)(df.DataStartOffset)
            });

            return df;
        }

        public void Dispose()
        {
            this.MemoryDataFile.Dispose();
        }
    }
}
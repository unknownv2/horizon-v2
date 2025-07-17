using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Modern_Warfare_3
{
    abstract class Segment
    {
        public void Read(byte[] data)
        {
            this.Read(new EndianIO(data, EndianType.BigEndian, true));
        }

        public abstract void Read(EndianIO io);
        public abstract byte[] ToArray();

        protected List<string> readStringBlock(int count, EndianIO IO)
        {
            List<string> stringList = new List<string>();

            short stringLength;
            while (count-- > 0)
                if ((stringLength = IO.In.ReadInt16()) != 0 && stringLength < 0x400)
                    stringList.Add(Encoding.ASCII.GetString(IO.In.ReadBytes(stringLength)));
                else
                    stringList.Add(string.Empty);

            return stringList;
        }

        protected void writeStringBlock(int count, EndianIO IO, List<string> stringList)
        {
            short zero = 0x00;

            int numStrings = stringList.Count;
            for (int x = 0; x < count; x++)
            {
                if (stringList[x].Length != 0 && stringList[x].Length < 0x400)
                {
                    IO.Out.Write((short)stringList[x].Length);
                    IO.Out.Write(Encoding.ASCII.GetBytes(stringList[x]));
                }                
                else
                    IO.Out.Write(zero);
            }
        }
    }
}

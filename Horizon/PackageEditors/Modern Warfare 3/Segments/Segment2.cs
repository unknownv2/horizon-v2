using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Horizon.PackageClasses;

namespace Horizon.PackageEditors.Modern_Warfare_3
{
    class Segment2 : Segment
    {
        private byte[] header, postDvars1, footer;
        private int u1, u2, u3, u4;

        public List<string> models1, models2,
            effects1, effects2,
            audio1, audio2,
            text1, uList1, uList2;

        public DvarCollection Dvars;

        public override void Read(EndianIO io)
        {
            header = io.In.ReadBytes(0xDB);

            models1 = readStringBlock(0x400, io);
            models2 = readStringBlock(0x20, io);
            effects1 = readStringBlock(0x200, io);
            effects2 = readStringBlock(0x100, io);
            audio1 = readStringBlock(0x200, io);
            audio2 = readStringBlock(0x80, io);
            text1 = readStringBlock(0x4B0, io);
            uList1 = readStringBlock(0x02, io);
            uList2 = readStringBlock(0x0D, io);

            this.Dvars = new DvarCollection(io);

            postDvars1 = io.In.ReadBytes(0xAC00);
            u1 = io.In.ReadInt32();
            u2 = io.In.ReadInt32();
            u3 = io.In.ReadInt32();
            u4 = io.In.ReadInt32();

            io.In.ReadInt32();

            footer = io.In.ReadBytes(io.Length - io.Position);
        }

        public override byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(header);

            writeStringBlock(0x400, IO, models1);
            writeStringBlock(0x20, IO, models2);
            writeStringBlock(0x200, IO, effects1);
            writeStringBlock(0x100, IO, effects2);
            writeStringBlock(0x200, IO, audio1);
            writeStringBlock(0x80, IO, audio2);
            writeStringBlock(0x4B0, IO, text1);
            writeStringBlock(0x02, IO, uList1);
            writeStringBlock(0x0D, IO, uList2);

            this.Dvars.Write(IO);

            IO.Out.Write(postDvars1);
            IO.Out.Write(u1);
            IO.Out.Write(u2);
            IO.Out.Write(u3);
            IO.Out.Write(u4);

            if (this.Dvars.ContainsKey("g_player_maxHealth"))
                IO.Out.Write(int.Parse(this.Dvars["g_player_maxHealth"]));
            else
                IO.Out.Write(100);

            IO.Out.Write(footer);

            IO.Close();
            return IO.ToArray();
        }
    }
}

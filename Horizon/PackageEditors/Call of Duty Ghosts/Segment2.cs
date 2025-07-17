using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horizon.PackageClasses;
using Horizon.PackageEditors.Modern_Warfare_3;

namespace Horizon.PackageEditors.Call_of_Duty_Ghosts
{
    class Segment2 : Segment
    {
        private byte[] header, postDvars1, postDvars2, footer;
        private int u1, u2, u3, u4, u5;

        public List<string> models1, models2,
            effects1, effects2,
            audio1, audio2,
            text1, uList1, uList2;

        public DvarCollection Dvars;

        public override void Read(EndianIO io)
        {
            header = io.In.ReadBytes(0xCE);

            models1 = readStringBlock(0x400, io);
            models2 = readStringBlock(0x20, io);
            effects1 = readStringBlock(0x200, io);
            effects2 = readStringBlock(0x200, io);
            audio1 = readStringBlock(0x200, io);
            audio2 = readStringBlock(0x80, io);
            text1 = readStringBlock(0x834, io);
            uList1 = readStringBlock(0x02, io);
            uList2 = readStringBlock(0x0E, io);

            Dvars = new DvarCollection(io);

            postDvars1 = io.In.ReadBytes(0xAC00);

            u1 = io.In.ReadInt32();
            postDvars2 = io.In.ReadBytes(u1 << 3);
            u2 = io.In.ReadInt32();
            u3 = io.In.ReadInt32();
            u4 = io.In.ReadInt32();
            u5 = io.In.ReadInt32();

            io.In.ReadInt32();

            footer = io.In.ReadBytes(io.Length - io.Position);
        }

        public override byte[] ToArray()
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(header);

            writeStringBlock(0x400, io, models1);
            writeStringBlock(0x20, io, models2);
            writeStringBlock(0x200, io, effects1);
            writeStringBlock(0x200, io, effects2);
            writeStringBlock(0x200, io, audio1);
            writeStringBlock(0x80, io, audio2);
            writeStringBlock(0x834, io, text1);
            writeStringBlock(0x02, io, uList1);
            writeStringBlock(0x0E, io, uList2);

            Dvars.Write(io);

            io.Out.Write(postDvars1);
            io.Out.Write(u1);
            io.Out.Write(postDvars2);
            io.Out.Write(u2);
            io.Out.Write(u3);
            io.Out.Write(u4);
            io.Out.Write(u5);

            io.Out.Write(Dvars.ContainsKey("g_player_maxHealth") ? int.Parse(Dvars["g_player_maxHealth"]) : 100);

            io.Out.Write(footer);

            io.Close();
            return io.ToArray();
        }
    }
}

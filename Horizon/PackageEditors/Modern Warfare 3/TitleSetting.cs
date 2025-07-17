using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Modern_Warfare_3
{
    public enum ValueType : byte
    {
        Bool = 0x02,
        Int32 = 0x04,
        Float = 0x05,
        String = 0x06,
        Array = 0x07
    }

    public struct SettingEntry
    {
        public ValueType Type;
        public object Value;
        public short Unknown;
        public short ID;

        public string String { get { return (string)this.Value; } }
        public int Int32 { get { return (int)this.Value; } }
        public bool Boolean { get { return (bool)this.Value; } }
        public float Float { get { return (float)this.Value; } }
        public byte[] Array { get { return (byte[])this.Value; } }
    }

    class TitleSetting : List<SettingEntry>
    {
        private int Unknown1, Unknown2;
        public TitleSetting(EndianIO IO)
        {
            if (IO.In.ReadUInt32() != 0x53454D56)
                throw new Exception("Invalid Title Setting magic!");

            this.Unknown1 = IO.In.ReadInt32();
            this.Unknown2 = IO.In.ReadInt32();

            while (IO.Stream.Position != IO.Stream.Length)
            {
                if (IO.In.ReadByte() != this.Count)
                    throw new Exception("Values not in order!");

                SettingEntry entry = new SettingEntry();
                entry.Type = (ValueType)IO.In.ReadByte();

                switch (entry.Type)
                {
                    case ValueType.Bool:
                        entry.Value = IO.In.ReadBoolean();
                        break;
                    case ValueType.Float:
                        entry.Value = IO.In.ReadSingle();
                        break;
                    case ValueType.Int32:
                        entry.Value = IO.In.ReadInt32();
                        break;
                    case ValueType.String:
                        entry.Unknown = IO.In.ReadInt16();
                        entry.ID = IO.In.ReadInt16();
                        entry.Value = IO.In.ReadNullTerminatedString();
                        break;
                    case ValueType.Array:
                        entry.Unknown = IO.In.ReadInt16();
                        entry.Value = IO.In.ReadBytes(IO.In.ReadInt16());
                        break;
                }

                this.Add(entry);
            }
        }

        public string GetStringByID(short stringId)
        {
            object obj = this.GetObject(ValueType.String, stringId);
            if (obj == null)
                return null;
            return (string)obj;
        }

        public byte[] GetArrayByID(short arrayId)
        {
            object obj = this.GetObject(ValueType.Array, arrayId);
            if (obj == null)
                return null;
            return (byte[])obj;
        }

        private object GetObject(ValueType valueType, short valueId)
        {
            for (int x = 0; x < this.Count; x++)
                if (this[x].Type == valueType && this[x].ID == valueId)
                    return this[x].Value;
            return null;
        }

        public new byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian);
            IO.Out.Write(0x53454D56);
            IO.Out.Write(this.Unknown1);
            IO.Out.Write(this.Unknown2);

            for (byte x = 0; x < this.Count; x++)
            {
                IO.Out.Write(x);
                IO.Out.Write((byte)this[x].Type);

                switch (this[x].Type)
                {
                    case ValueType.Bool:
                        IO.Out.Write(this[x].Boolean);
                        break;
                    case ValueType.Int32:
                        IO.Out.Write(this[x].Int32);
                        break;
                    case ValueType.Float:
                        IO.Out.Write(this[x].Float);
                        break;
                    case ValueType.String:
                        IO.Out.Write(this[x].Unknown);
                        IO.Out.Write(this[x].ID);
                        IO.Out.WriteAsciiString(this[x].String, this[x].String.Length + 1);
                        break;
                    case ValueType.Array:
                        IO.Out.Write(this[x].Unknown);
                        IO.Out.Write((short)this[x].Array.Length);
                        IO.Out.Write(this[x].Array);
                        break;
                }
            }

            IO.Close();
            return IO.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AssetStudio
{
    public enum EndianType
    {
        LittleEndian,
        BigEndian
    }

    public class EndianBinaryReader : BinaryReader
    {
        public EndianType endian;

        public EndianBinaryReader(Stream stream, EndianType endian = EndianType.BigEndian) : base(stream)
        {
            this.endian = endian;
        }

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public override short ReadInt16()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(2);
                Array.Reverse(buff);
                return BitConverter.ToInt16(buff, 0);
            }
            return base.ReadInt16();
        }

        public override int ReadInt32()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToInt32(buff, 0);
            }
            return base.ReadInt32();
        }

        public override long ReadInt64()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToInt64(buff, 0);
            }
            return base.ReadInt64();
        }

        public override ushort ReadUInt16()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(2);
                Array.Reverse(buff);
                return BitConverter.ToUInt16(buff, 0);
            }
            return base.ReadUInt16();
        }

        public override uint ReadUInt32()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToUInt32(buff, 0);
            }
            return base.ReadUInt32();
        }

        public override ulong ReadUInt64()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToUInt64(buff, 0);
            }
            return base.ReadUInt64();
        }

        public override float ReadSingle()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToSingle(buff, 0);
            }
            return base.ReadSingle();
        }

        public override double ReadDouble()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToUInt64(buff, 0);
            }
            return base.ReadDouble();
        }

        public override string ToString()
        {
            return $"<{Position}>" + base.ToString();
        }

        public byte[] Dump()
        {
            var pos = Position;
            Position = 0;
            var buf = new byte[pos];
            Read(buf, 0, (int)pos);
            return buf;
        }

        public byte[] Dump(int len)
        {
            var pos = Position;
            var buf = new byte[len];
            Read(buf, 0, len);
            Position = pos;
            return buf;
        }
    }



    public class EndianBinaryWriter : BinaryWriter
    {
        public EndianType endian;

        public EndianBinaryWriter(Stream stream, EndianType endian = EndianType.BigEndian) : base(stream)
        {
            this.endian = endian;
        }

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }


        public override void Write(Int16 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);        
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(Int32 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);        
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(Int64 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);        
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(UInt16 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);         
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(UInt32 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);         
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(UInt64 v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);         
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(Single v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);         
            }
            base.Write(buff, 0, buff.Length);
        }

        public override void Write(Double v)
        {
            var buff = BitConverter.GetBytes(v);
            if (endian == EndianType.BigEndian)
            {
                Array.Reverse(buff);         
            }
            base.Write(buff, 0, buff.Length);
        }

        public override string ToString()
        {
            return $"<{Position}>" + base.ToString();
        }

        public byte[] Dump()
        {
            var pos = Position;
            Position = 0;
            var buf = new byte[pos];
            BaseStream.Read(buf, 0, (int)pos);
            return buf;
        }
    }

}

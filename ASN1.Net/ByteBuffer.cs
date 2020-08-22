using System;

namespace ASN1
{
    public class ByteBuffer
    {
        private byte[] _buffer;
        public int Offset { get; private set; }

        public ByteBuffer(byte[] buffer)
        {
            _buffer = buffer;
        }

        public byte[] ReadBytes(int count)
        {
            byte[] subBuffer = new byte[count];
            Buffer.BlockCopy(_buffer, Offset, subBuffer, 0, count);
            Offset += count;
            return subBuffer;
        }

        public byte ReadByte()
        {
            byte b = _buffer[Offset];
            Offset++;
            return b;
        }
    }
}

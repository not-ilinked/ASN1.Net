using System;

namespace ASN1
{
    internal class ByteBuffer
    {
        private readonly byte[] _buffer;
        public int Offset { get; set; }

        public ByteBuffer(byte[] buffer)
        {
            _buffer = buffer;
        }

        public byte[] ReadBytes(int count)
        {
            CheckRemainingBytes(count);

            byte[] subBuffer = new byte[count];
            Buffer.BlockCopy(_buffer, Offset, subBuffer, 0, count);
            Offset += count;
            return subBuffer;
        }

        public byte ReadByte()
        {
            CheckRemainingBytes(1);

            byte b = _buffer[Offset];
            Offset++;
            return b;
        }

        private void CheckRemainingBytes(int bytesRequired)
        {
            if (_buffer.Length - Offset < bytesRequired)
                throw new InvalidOperationException($"Less than {bytesRequired} bytes are left");
        }
    }
}

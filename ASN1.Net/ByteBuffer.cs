using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASN1
{
    public class ByteBuffer
    {
        private byte[] _buffer;
        private int _offset;

        public ByteBuffer(byte[] buffer)
        {
            _buffer = buffer;
        }

        public byte[] ReadBytes(int count)
        {
            byte[] subBuffer = new byte[count];
            Buffer.BlockCopy(_buffer, _offset, subBuffer, 0, count);
            _offset += count;
            return subBuffer;
        }

        public byte ReadByte()
        {
            byte b = _buffer[_offset];
            _offset++;
            return b;
        }
    }
}

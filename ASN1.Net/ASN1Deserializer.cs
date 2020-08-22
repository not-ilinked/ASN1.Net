using System;
using System.Collections;
using System.Collections.Generic;

namespace ASN1
{
    public static class ASN1Deserializer
    {
        private static byte ExtractBits(BitArray source, int offset, int count)
        {
            BitArray copied = new BitArray(source.Count);

            for (int i = 0; i < count; i++)
                copied[i] = source[i + offset];

            byte[] bytes = new byte[copied.Length / 8];
            copied.CopyTo(bytes, 0);
            return bytes[0];
        }


        private static List<ASN1Object> Deserialize(ByteBuffer buffer, int count)
        {
            List<ASN1Object> objects = new List<ASN1Object>();
            int endAt = buffer.Offset + count;

            while (buffer.Offset < endAt)
            {
                BitArray flags = new BitArray(new byte[] { buffer.ReadByte() });

                byte tag = ExtractBits(flags, 0, 5);
                ASN1ObjectType type = (ASN1ObjectType)ExtractBits(flags, 5, 1);
                byte @class = ExtractBits(flags, 6, 1);
                int contentLength = GetContentLengh(buffer);

                if (type == ASN1ObjectType.Primitive)
                {
                    byte[] contents = buffer.ReadBytes(contentLength);

                    if (@class == 0 && (UniversalTagType)tag == UniversalTagType.BitString)
                        objects.Add(new ASN1BitString(contents));
                    else
                        objects.Add(new ASN1PrimitiveObject(@class, tag, contents));
                }
                else
                    objects.Add(new ASN1StructuredObject(Deserialize(buffer, contentLength), @class, tag));
            }

            return objects;
        }


        private static int GetContentLengh(ByteBuffer buffer)
        {
            int len = buffer.ReadByte();

            const int lenCap = 128;

            if (len == lenCap)
                throw new NotImplementedException("The deserializer does currently not have an implementation for unknown lengths");
            else if (len > lenCap)
            {
                int extraBytes = len - lenCap;

                if (extraBytes > sizeof(int))
                    throw new NotImplementedException("The deserializer can currently only handle length byte amounts up to 4");

                byte[] withPadding = new byte[sizeof(int)];

                byte[] lenBytes = buffer.ReadBytes(extraBytes);
                Buffer.BlockCopy(lenBytes, 0, withPadding, 0, lenBytes.Length);

                len = BitConverter.ToInt32(withPadding, 0);
            }

            return len;
        }


        public static List<ASN1Object> Deserialize(byte[] encodedData)
        {
            return Deserialize(new ByteBuffer(encodedData), encodedData.Length);
        }
    }
}

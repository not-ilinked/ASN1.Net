using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] lol = Convert.FromBase64String("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCqGKukO1De7zhZj6+H0qtjTkVxwTCpvKe4eCZ0FPqri0cb2JZfXJ/DgYSF6vUpwmJG8wVQZKjeGcjDOL5UlsuusFncCzWBQ7RKNUSesmQRMSGkVb1/3j+skZ6UtW+5u09lHNsj6tQ51s1SPrCBkedbNf0Tp0GbMJDyR4e9T04ZZwIDAQAB");

            BitArray flags = new BitArray(new byte[] { lol[0] });
            Reverse(flags);

            BitArray tag = CopyBits(flags, 3, 5);

            byte[] asArray = new byte[tag.Length / 8];
            tag.CopyTo(asArray, 0);
        }

        private static void Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }


        private static BitArray CopyBits(BitArray source, int offset, int count)
        {
            BitArray copied = new BitArray(source.Count);

            for (int i = 0; i < count; i++)
                copied[i] = source[i + offset];

            return copied;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASN1;

namespace test
{
    class EncryptionInfo
    {
        [ASN1Property(UniversalTag.ObjectIdentifier)]
        public byte[] Identifier { get; private set; }
    }

    class KeyInfo
    {
        [ASN1Property(UniversalTag.Integer)]
        public byte[] Key { get; private set; }
        
        [ASN1Property(UniversalTag.Integer)]
        public byte[] Exponent { get; private set; }
    }

    class Container
    {
        [ASN1Property(UniversalTag.Sequence)]
        public ASN1StructuredObject Info { get; private set; }
        
        [ASN1Property(UniversalTag.BitString)]
        private byte[] _keyBytes { get; set; }

        public KeyInfo Key
        {
            get
            {
                return ASN1Deserializer.Deserialize(_keyBytes)[0].ToObject<KeyInfo>();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = Convert.FromBase64String("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCjovQt7dGeCsMt6vmSQK3XF57LSi0jPCj7FwXX799MmTM6eYlBlwQbsYJH6xPuft0l6zsLp2jL6A/33IP1gw3EEjR9q8rGZHhMfwVyC+Pq0c42nfc5HMzLdm3qUQdXl3TuX3+DW13ZLmv70Da51dtGvjFqYh5wJnt53ACTt6OyWwIDAQAB");

            ASN1StructuredObject container = (ASN1StructuredObject)ASN1Deserializer.Deserialize(data)[0];

            var converted = container.ToObject<Container>();
        }
    }
}

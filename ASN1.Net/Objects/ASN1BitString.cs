using System;

namespace ASN1
{
    // note: this impl currently doesn't do anything with the amount of removed bits cuz idk what the fuck it means lol
    public class ASN1BitString : ASN1PrimitiveObject
    {
        public byte UnusedBits { get; private set; }
        private byte[] _string;

        public override byte[] GetValue()
        {
            return _string;
        }

        public ASN1BitString(byte[] value) : base(0, (int)UniversalTagType.BitString)
        {
            RawValue = value;

            UnusedBits = value[0];

            _string = new byte[value.Length - 1];
            Buffer.BlockCopy(value, 1, _string, 0, _string.Length);
        }
    }
}

using System;

namespace ASN1
{
    public class ASN1Object
    {
        public int Class { get; private set; }
        public int Tag { get; private set; }
        public ASN1ObjectType Type { get; private set; }

        public bool UniversalClass
        {
            get
            {
                return Class == 0;
            }
        }

        public ASN1Object(ASN1ObjectType type, int cls, int tag)
        {
            Type = type;
            Class = cls;
            Tag = tag;
        }

        public UniversalTagType GetUniversalTag()
        {
            if (UniversalClass)
                return (UniversalTagType)Tag;
            else
                throw new InvalidOperationException("Class must be of type universal (0)");
        }
    }
}

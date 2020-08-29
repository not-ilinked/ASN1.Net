using System;

namespace ASN1
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ASN1PropertyAttribute : Attribute
    {
        public int Class { get; private set; }
        public int Tag { get; private set; }

        public ASN1PropertyAttribute(int cls, int tag)
        {
            Class = cls;
            Tag = tag;
        }

        public ASN1PropertyAttribute(UniversalTag tag) : this(0, (int)tag)
        { }
    }
}

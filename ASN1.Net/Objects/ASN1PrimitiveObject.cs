namespace ASN1
{
    public class ASN1PrimitiveObject : ASN1Object
    {
        protected byte[] RawValue { get; set; }

        public virtual byte[] GetValue()
        {
            return RawValue;
        }

        public ASN1PrimitiveObject(int cls, int tag, byte[] value) : base(ASN1ObjectType.Primitive, cls, tag)
        {
            RawValue = value;
        }

        // for universal types that require special handling
        protected ASN1PrimitiveObject(int cls, int tag) : this(cls, tag, null)
        { }
    }
}

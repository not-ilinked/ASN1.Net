namespace ASN1
{
    public class ASN1PrimitiveObject : ASN1Object
    {
        public byte[] Value { get; private set; }

        public ASN1PrimitiveObject(int cls, int tag, byte[] value) : base(ASN1ObjectType.Primitive, cls, tag)
        {
            Value = value;
        }
    }
}

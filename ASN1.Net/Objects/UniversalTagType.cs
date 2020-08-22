namespace ASN1
{
    // https://www.obj-sys.com/asn1tutorial/node124.html
    public enum UniversalTagType
    {
        ReservedForBER,
        Bool,
        Int,
        BitString,
        OctetString,
        Null,
        ObjectIdentifier,
        ObjectDescriptor,
        InstanceOfOrExternal,
        Real,
        Enumerated,
        EmbeddedPDV,
        UTF8String,
        RelativeOid,
        Sequence = 16,
        Set,
        NumericString,
        PrintableString,
        TeletexString,
        VideotexString,
        IA5String,
        UTCTime,
        GeneralizedTime,
        GraphicString,
        ISO646String,
        GeneralString,
        UniversalString,
        CharacterString,
        BMPString
    }
}

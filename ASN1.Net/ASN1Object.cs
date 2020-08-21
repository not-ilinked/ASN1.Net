using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASN1
{
    public class ASN1Object
    {
        public int Class { get; private set; }
        public int Tag { get; private set; }
        public ASN1ObjectType Type { get; private set; }

        public ASN1Object(ASN1ObjectType type, int cls, int tag)
        {
            Type = type;
            Class = cls;
            Tag = tag;
        }
    }
}

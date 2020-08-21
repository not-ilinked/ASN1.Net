using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASN1
{
    public class ASN1StructuredObject : ASN1Object
    {
        public IReadOnlyList<ASN1Object> Children { get; private set; }

        public ASN1StructuredObject(List<ASN1Object> children, int cls, int tag) : base(ASN1ObjectType.Structured, cls, tag)
        {
            Children = children;
        }
    }
}

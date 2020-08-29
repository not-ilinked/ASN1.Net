using System;
using System.Collections.Generic;
using System.Reflection;

namespace ASN1
{
    public class ASN1StructuredObject : ASN1Object
    {
        public IReadOnlyList<ASN1Object> Children { get; private set; }

        public ASN1StructuredObject(List<ASN1Object> children, int cls, int tag) : base(ASN1ObjectType.Structured, cls, tag)
        {
            Children = children;
        }

        public override object ToObject(Type type)
        {
            object inst = Activator.CreateInstance(type);

            List<ASN1Object> children = new List<ASN1Object>(Children);

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                foreach (var attr in property.GetCustomAttributes(false))
                {
                    if (attr.GetType() == typeof(ASN1PropertyAttribute))
                    {
                        ASN1PropertyAttribute asn1Attr = (ASN1PropertyAttribute)attr;

                        foreach (var child in children)
                        {
                            if (child.Class == asn1Attr.Class && child.Tag == asn1Attr.Tag)
                            {
                                if (property.PropertyType == child.GetType())
                                    property.SetValue(inst, child);
                                else
                                    property.SetValue(inst, child.ToObject(property.PropertyType));

                                children.Remove(child);

                                break;
                            }
                        }

                        break;
                    }
                }
            }

            return inst;
        }
    }
}

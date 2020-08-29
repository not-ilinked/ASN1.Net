using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ASN1
{
    public class ASN1PrimitiveObject : ASN1Object
    {
        private static Func<byte[], object> GetNumericHandler<T>(int expectedLength, Func<byte[], int, T> internalHandler)
        {
            return (byte[] buffer) => 
            {
                byte[] withPadding = new byte[expectedLength];
                Buffer.BlockCopy(buffer, 0, withPadding, 0, buffer.Length);

                return internalHandler(withPadding, 0);
            };
        }

        private static Dictionary<Type, Func<byte[], object>> _numericHandlers;
            
            /*new Dictionary<Type, Func<byte[], object>>()
        {
            { typeof(int), GetNumericHandler(sizeof(int), BitConverter.ToInt32) },
            { typeof(uint), GetNumericHandler(sizeof(uint), BitConverter.ToUInt32) },
            { typeof(short), GetNumericHandler(sizeof(short), BitConverter.ToInt16) },
            { typeof(ushort), GetNumericHandler(sizeof(ushort), BitConverter.ToUInt16) }
        };*/

        private static Dictionary<Type, Func<byte[], object>> LoadNumericHandlers()
        {
            var dict = new Dictionary<Type, Func<byte[], object>>();

            foreach (var method in typeof(BitConverter).GetMethods())
            {
                if (method.Name.StartsWith("To") && !method.Name.EndsWith("String"))
                {
                    int size = Marshal.SizeOf(t: method.ReturnType);

                    dict[method.ReturnType] = (byte[] buffer) =>
                    {
                        byte[] withPadding = new byte[size];
                        Buffer.BlockCopy(buffer, 0, withPadding, 0, buffer.Length);

                        return method.Invoke(null, new object[] { withPadding, 0 });
                    };
                }
            }

            return dict;
        }

        protected byte[] RawValue { get; set; }

        public virtual byte[] GetValue()
        {
            return RawValue;
        }

        public ASN1PrimitiveObject(int cls, int tag, byte[] value) : base(ASN1ObjectType.Primitive, cls, tag)
        {
            RawValue = value;

            if (_numericHandlers == null)
                _numericHandlers = LoadNumericHandlers();
        }

        // for universal types that require special handling
        protected ASN1PrimitiveObject(int cls, int tag) : this(cls, tag, null)
        { }

        public override object ToObject(Type type)
        {
            if (type == this.GetType())
                return this;
            else if (_numericHandlers.ContainsKey(type))
                return _numericHandlers[type](GetValue());
            else
                return Convert.ChangeType(GetValue(), type);
        }
    }
}

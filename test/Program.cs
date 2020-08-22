using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASN1;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] lol = Convert.FromBase64String("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCkfyBBdkzkt67EmWPJsW2ti6w/gC+vQtNLOXwL7GtRUuKO5DBHNHGESZazLcSP8Kc0D+8ccQVpQ7VPv7tUHmBPlOdygvW6FWKPz1HiGbjdTyhs4nqssg9lDtLCq86AqYtR+WrB2WRkUIZWlU/E5fOLoMgjo33WZU8K/nw4PjmUcQIDAQAB");

            var meme = ASN1Deserializer.Deserialize(lol);

            var container = (ASN1StructuredObject)meme[0];

            var bitStr = (ASN1BitString)container.Children[1];

            var kek = ASN1Deserializer.Deserialize(bitStr.GetValue());
        }
    }
}

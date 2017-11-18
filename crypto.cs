using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SharpOS.Cryptography
{
    class md5
    {
        public static string getHash(string str)
        {
            MD5 ToMD5 = MD5.Create();
            byte[] md5pass = ToMD5.ComputeHash(GenerateStream.FromString(str));
            string also = "";
            foreach (byte temp in md5pass)
            {
                also += temp;
            }
            return also;
        }
    }
}

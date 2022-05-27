using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EHWorld.Models
{
    public class hash {
        private String key;
        public hash(String key)
        {
            this.key = key;
        }
        public String ApplyHash(String word)
        {
            byte[] data = ASCIIEncoding.Unicode.GetBytes(word+key);
            byte[] result;
#pragma warning disable SYSLIB0021 // 类型或成员已过时
            SHA512 shaM = new SHA512Managed();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
            result = shaM.ComputeHash(data);
            return ASCIIEncoding.Unicode.GetString(result);
        }
    }
}
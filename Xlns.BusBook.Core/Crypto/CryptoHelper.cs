using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Xlns.BusBook.Core.Crypto
{
    public class CryptoHelper
    {
        public string cryptPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] hashPassword = md5.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < hashPassword.Length; i++)
                res.Append(hashPassword[i].ToString());
            return res.ToString();
        }
    }
}

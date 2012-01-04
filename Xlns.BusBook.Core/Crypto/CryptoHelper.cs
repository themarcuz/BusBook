using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Xlns.BusBook.Core.Crypto
{
    public class CryptoHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Effettua la criptazione della password sfruttando l'algoritmo di criptazione MD5
        /// </summary>
        /// <param name="password">Stringa da criptare</param>
        /// <returns>Stringa criptata</returns>
        public string cryptPassword(string password)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hashPassword = md5.ComputeHash(Encoding.Default.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashPassword.Length; i++)
                    builder.Append(hashPassword[i].ToString());
                var result = builder.ToString();
                logger.Debug("Password criptata con successo - hash = {0}", result);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}

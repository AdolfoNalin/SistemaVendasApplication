using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendasAplication.Helpers
{
    public class GenerationPassword
    {
        #region GenerationPassword
        public static string Generation(string password)
        {
            string hashPassword;

            using (SHA256 pass = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] result = pass.ComputeHash(bytes);
                hashPassword = Encoding.UTF8.GetString(result);
            }

            return hashPassword;
        }
        #endregion
    }
}
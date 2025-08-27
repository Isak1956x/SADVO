using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.Helpers
{
    public class PasswordEncriptation
    {
        public static string ComputeSha256Hash(string paswword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //ComputeHash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(paswword));
                //Convert byte array to a string
                StringBuilder sb = new();
                foreach (var item in bytes)
                {
                    sb.Append(item.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace RIPE.CrossCutting.Extensions
{
    public static class StringExtension
    {
        public static string GenerateSha256Hash(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            using SHA256Managed hasher = new SHA256Managed();
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace CashMachine
{
    public static class Security
    {
        public static string GetHashString(string psw)
        {
            using var sha256 = SHA256.Create();
            return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(psw))).Replace("-", "").ToLower();
        }
    }
}

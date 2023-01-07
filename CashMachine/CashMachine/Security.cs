using System.Security.Cryptography;
using System.Text;

namespace CashMachine
{
    public static class Security
    {
        public static string GetHashString(string psw)
        {
            return BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(psw))).Replace("-", "").ToLower();
        }
    }
}

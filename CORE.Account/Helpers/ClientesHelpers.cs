using System.Security.Cryptography;
using System.Text;

namespace CORE.Account.Helpers
{
    internal static class ClientesHelpers
    {

        public static string Cifrar(string valor)
        {
            //SHA256 md5Hash = SHA256.Create();
            MD5 md5Hash = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            byte[] hash = md5Hash.ComputeHash(data);
            string md5HashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return md5HashString;
        }
    }
}
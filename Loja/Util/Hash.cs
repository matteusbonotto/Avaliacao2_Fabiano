using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Hash
    {
        public static string SHA3(string texto)
        {
            var sha = new Sha3Digest(512);
            var input = Encoding.ASCII.GetBytes(texto);
            sha.BlockUpdate(input, 0, input.Length);
            var result = new byte[64];
            sha.DoFinal(result, 0);

            return BitConverter.ToString(result).ToLower();
        }
        public static string Salt(string texto)
        {
            string pass = null;
            foreach (var item in texto.Split('-'))
            {
                pass += Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(item));
            }

            return SHA3(pass).Replace("-", string.Empty);
        }
    }
}

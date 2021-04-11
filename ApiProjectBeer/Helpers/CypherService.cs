using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeer.Helpers
{
    public class CypherService
    {
        public static String GetSalt()
        {
            Random random = new Random();
            String salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }
        public static byte[] CypherContent(string contenido, string salt)
        {
            string contenidosalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(contenidosalt);
            //CIFRAMOS EL NUMERO DE ITERACCIONES QUE NOS INDIQUEN
            for (int i = 1; i <= 101; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
        public static byte[] CypherContentNoSalt(string contenido)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(contenido);
            //CIFRAMOS EL NUMERO DE ITERACCIONES QUE NOS INDIQUEN
            for (int i = 1; i <= 101; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}

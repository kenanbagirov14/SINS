using System.Security.Cryptography;
using System.Text;

namespace NIS.UtilsCore
{
    public class CHashing
    {    
        public static string RandomSalt( string input)
        {
            MD5 md5Hash = MD5.Create();
            
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static string Hash( string password,string salt)
        {
            // Hash the input.
            string hashOfInput = RandomSalt(password+salt);

            return hashOfInput;
        }
    }
}

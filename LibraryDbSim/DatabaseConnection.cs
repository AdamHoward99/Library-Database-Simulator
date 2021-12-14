using System.Configuration;
using MySqlConnector;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LibraryDbSim
{
    class DatabaseConnection
    {
        public static MySqlConnection conn = new MySqlConnection(DecryptCipherTextToText(ConfigurationManager.ConnectionStrings["MySQLConn"].ConnectionString));
        public static MySqlCommand cmd = new MySqlCommand("", conn);
        public static MySqlDataReader reader;
        private const string key = "JN34S0WN47_12121";

        public static void CloseAll()      //Disconnects both reader and conn variables
        {
            reader.Close();
            conn.Close();
        }

        public static bool TryConnection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }

            return true;
        }

        public static string EncryptTextToCipher(string text)
        {
            //Get the bytes of the text
            byte[] encryptArray = Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            //Obtaining bytes from security key
            byte[] securityKeyArray = provider.ComputeHash(Encoding.UTF8.GetBytes(key));

            //Deallocate Memory after operation
            provider.Clear();
            TripleDESCryptoServiceProvider TripleDESService = new TripleDESCryptoServiceProvider
            {
                Key = securityKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform transform = TripleDESService.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(encryptArray, 0, encryptArray.Length);
            TripleDESService.Clear();

            return Convert.ToBase64String(result, 0, result.Length);
        }

        public static string DecryptCipherTextToText(string cipherText)
        {
            byte[] encryptArray = Convert.FromBase64String(cipherText);
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            //Obtaining Bytes from key
            byte[] securityKeyArray = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Clear();

            TripleDESCryptoServiceProvider TripleDESService = new TripleDESCryptoServiceProvider
            {
                Key = securityKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform transform = TripleDESService.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(encryptArray, 0, encryptArray.Length);
            TripleDESService.Clear();

            return Encoding.UTF8.GetString(result);
        }

    }
}

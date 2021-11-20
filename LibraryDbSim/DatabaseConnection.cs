using System.Configuration;
using MySqlConnector;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LibraryDbSim
{
    class DatabaseConnection
    {
        public static MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConn"].ConnectionString);
        public static MySqlCommand cmd = new MySqlCommand("", conn);
        public static MySqlDataReader reader;
        private const string key = "JN34S0WN47_12121";
        public static void CloseAll()      //Disconnects both reader and conn variables
        {
            reader.Close();
            conn.Close();
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
            var TripleDESService = new TripleDESCryptoServiceProvider();
            TripleDESService.Key = securityKeyArray;
            TripleDESService.Mode = CipherMode.ECB;
            TripleDESService.Padding = PaddingMode.PKCS7;

            var transform = TripleDESService.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(encryptArray, 0, encryptArray.Length);
            TripleDESService.Clear();

            return Convert.ToBase64String(result, 0, result.Length);
        }

    }
}

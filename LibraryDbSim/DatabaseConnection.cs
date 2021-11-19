using System.Configuration;
using MySqlConnector;

namespace LibraryDbSim
{
    class DatabaseConnection
    {
        public static MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConn"].ConnectionString);
        public static MySqlCommand cmd = new MySqlCommand("", conn);
        public static MySqlDataReader reader;
        public static void CloseAll()      //Disconnects both reader and conn variables
        {
            reader.Close();
            conn.Close();
        }

    }
}

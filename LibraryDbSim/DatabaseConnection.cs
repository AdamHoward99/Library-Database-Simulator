using MySqlConnector;

namespace LibraryDbSim
{
    class DatabaseConnection
    {
        public static MySqlConnection conn = new MySqlConnection("Server = 127.0.0.1; Database = librarydatabase; Uid =; Pwd =;");
        public static MySqlCommand cmd = new MySqlCommand("", conn);
        public static MySqlDataReader reader;

        public static void CloseAll()      //Disconnects both reader and conn variables
        {
            reader.Close();
            conn.Close();
        }

    }
}

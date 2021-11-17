namespace LibraryDbSim
{
    public class LibrarySystem
    {
        public bool AvailableEmailAddress(string email)
        {
            DatabaseConnection.conn.Open();

            DatabaseConnection.cmd.CommandText = "SELECT * FROM accounts WHERE (email = @email)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();

            if (DatabaseConnection.reader.HasRows)
            {
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.CloseAll();
                return false;       //Email is already in use
            }

            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.CloseAll();
            return true;
        }

        public void AddAccountToSystem(int age, string name, string email, string password)
        {
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "INSERT INTO accounts (email, password, name, age) VALUES(@email, @password, @name, @age)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.cmd.Parameters.AddWithValue("@password", password);
            DatabaseConnection.cmd.Parameters.AddWithValue("@name", name);
            DatabaseConnection.cmd.Parameters.AddWithValue("@age", age);
            DatabaseConnection.cmd.ExecuteNonQuery();
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.conn.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace LibraryDbSim
{
    public class LibrarySystem
    {
        public bool AccountValid(string email, string password, out int errorFlag)
        {
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "SELECT * FROM accounts WHERE (email = @email AND password = @password)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.cmd.Parameters.AddWithValue("@password", password);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();
            if(DatabaseConnection.reader.HasRows)     //Account found that matches information, account is valid
            {
                errorFlag = 1;
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.CloseAll();
                return true;
            }

            errorFlag = 0;
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.CloseAll();
            return false;
        }

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

        public bool CheckNewUserPassword(string email, string newPassword) 
        {
            DatabaseConnection.conn.Open();

            //Find user with selected email and make sure password is different
            DatabaseConnection.cmd.CommandText = "SELECT * FROM accounts WHERE (email = @email AND password != @password)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.cmd.Parameters.AddWithValue("@password", newPassword);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();
            if(DatabaseConnection.reader.HasRows)      //Account email has been found with a different password, reset the password
            {
                //Change the password
                DatabaseConnection.reader.Close();
                DatabaseConnection.cmd.CommandText = "UPDATE accounts SET password = @password WHERE email = @email";
                DatabaseConnection.cmd.ExecuteNonQuery();
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.conn.Close();
                return true;
            }

            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.CloseAll();
            return false;
        }
    }
}

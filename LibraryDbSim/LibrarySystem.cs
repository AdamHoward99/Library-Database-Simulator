﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace LibraryDbSim
{
    public class LibrarySystem
    {
        MySqlConnection connection = new MySqlConnection("Server = 127.0.0.1; Database = librarydatabase; Uid =; Pwd =;");

        public LibrarySystem()
        {
            this.Users = new List<Account>();
            CreateBookCollection();
        }

        public void AddDecoyAccounts()      //TODO: To be replaced by database eventually
        {
            Users.Add(new Account(22, "Adam", "example@gmail.com", "password"));
            Users[0].AddBookToList(BookCollection[0]);      //In future, would be stored on a db

            Users.Add(new Account(50, "Jon", "jon@gmail.com", "jon"));
        }

        public bool AccountValid(string email, string password, out int errorFlag)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM accounts WHERE (email = @email AND password = @password)", connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)     //Account found that matches information, account is valid
            {
                errorFlag = 1;
                connection.Close();
                return true;
            }

            errorFlag = 0;
            connection.Close();
            return false;
        }

        public bool AvailableEmailAddress(string email)
        {
            connection.Open();

            MySqlCommand selectCmd = new MySqlCommand("SELECT * FROM accounts WHERE (email = @email)", connection);
            selectCmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = selectCmd.ExecuteReader();
            if (reader.HasRows)
            {
                connection.Close();
                return false;       //Email is already in use
            }

            connection.Close();
            return true;
        }

        public void AddAccountToSystem(int age, string name, string email, string password)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO accounts (email, password, name, age) VALUES(@email, @password, @name, @age)", connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public bool CheckNewUserPassword(string email, string newPassword) 
        {
            connection.Open();

            //Find user with selected email and make sure password is different
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM accounts WHERE (email = @email AND password != @password)", connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", newPassword);
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)      //Account email has been found with a different password, reset the password
            {
                //Change the password
                reader.Close();
                cmd.CommandText = "UPDATE accounts SET password = @password WHERE email = @email";
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }

            connection.Close();
            return false;
        }

        public Account GetAccount(string email) => Users.Find(acc => acc.Email == email);       //Finds an account for account window based on email field

        public List<Book> GetBookCollection() => BookCollection;        //TODO: Find way around this, move book collection into its own class?

        private void CreateBookCollection()     //TODO: To be replaced by database eventually
        {
            BookCollection.Add(new Book("Of Mice and Men", "John Steinbeck", 4, new DateTime(1937, 11, 23), BookCategories.Fiction));
            BookCollection.Add(new Book("To Kill a Mockingbird", "Harper Lee", 1, new DateTime(1960, 7, 11), BookCategories.Fiction));
            BookCollection.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", 4, new DateTime(1925, 4, 10), BookCategories.Fiction));
            BookCollection.Add(new Book("The Invisible Man", "H. G. Wells", 1, new DateTime(1897, 1, 1), BookCategories.Horror));
            BookCollection.Add(new Book("Beloved", "Toni Morrison", 3, new DateTime(1987, 9, 1), BookCategories.Fiction));
            BookCollection.Add(new Book("The Hobbit", "J. R. R. Tolkien", 3, new DateTime(1937, 1, 1), BookCategories.Fantasy));
            BookCollection.Add(new Book("Harry Potter and the Philosopher's Stone", "J. K. Rowling", 5, new DateTime(1997, 6, 26), BookCategories.Fantasy));
            BookCollection.Add(new Book("The Lion, the Witch and the Wardrobe", "C. S. Lewis", 6, new DateTime(1950, 10, 16), BookCategories.Fiction));
            BookCollection.Add(new Book("Harry Potter and the Chamber of Secrets", "J. K. Rowling", 2, new DateTime(1998, 6, 2), BookCategories.Fiction));
            BookCollection.Add(new Book("Watership Down", "Richard Adams", 4, new DateTime(1972, 11, 1), BookCategories.Fantasy));
        }

        public void ReturnBook(Book book) => BookCollection.Find(b => b == book).Stock++;

        private List<Account> Users;
        private List<Book> BookCollection = new List<Book>();      //Could move to another class?
        private Random random = new Random();      //Used for simulating book renting (gets random book from collection)
    }
}

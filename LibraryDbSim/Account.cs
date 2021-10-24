using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    public class Account : IUser
    {
        public Account() {}
        public Account(int age, string name, string email, string pass)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
            this.Password = pass;
            this.CurrentRentedBooks = new List<Book>();
            this.PreviousRentedBooks = new List<Book>();
        }

        public void AddBookToList(Book book)
        {
            CurrentRentedBooks.Add(book);
        }

        //Used to output book list on account page
        public List<Book> GetCurrentRentedBooks() => CurrentRentedBooks;

        public void RemoveBookFromList(Book book)
        {
            CurrentRentedBooks.Remove(book);
        }

        public void RemoveBookFromList(int index) => CurrentRentedBooks.RemoveAt(index);        //Alt. version of above function, tracks item to remove via index from item list on account window

        public void ChangePassword(string newPassword) => Password = newPassword;

        //Variables
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        private int Age { get; set; }
        List<Book> CurrentRentedBooks;
        List<Book> PreviousRentedBooks;
    }
}

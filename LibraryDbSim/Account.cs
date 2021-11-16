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
        }

        public void AddBookToList(Book book)
        {
            CurrentRentedBooks.Add(book);
        }

        public void RemoveBookFromList(Book book)
        {
            CurrentRentedBooks.Remove(book);
        }


        //Variables
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        private int Age { get; set; }
        List<Book> CurrentRentedBooks;

        public int AccountID { get; set; }
    }
}

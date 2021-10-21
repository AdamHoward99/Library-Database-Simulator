using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    class Account : IUser
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

        public void RemoveBookFromList(Book book)
        {
            CurrentRentedBooks.Remove(book);
        }

        public void ChangePassword(string newPassword) => Password = newPassword;

        //Variables
        public string Email { get; private set; }
        public string Password { get; private set; }
        private string Name { get; set; }
        private int Age { get; set; }
        List<Book> CurrentRentedBooks;
        List<Book> PreviousRentedBooks;



    }
}

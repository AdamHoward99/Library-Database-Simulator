using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    public class LibrarySystem
    {
        public LibrarySystem()
        {
            this.Users = new List<Account>();
            CreateBookCollection();
        }

        public void AddDecoyAccounts()      //TODO: To be replaced by database eventually
        {
            Users.Add(new Account(22, "Adam", "example@gmail.com", "password"));
            Users.Add(new Account(50, "Jon", "jon@gmail.com", "jon"));
        }

        public bool AccountValid(string email, string password, out int errorFlag)
        {
            foreach(Account acc in Users)       //Searches each account stored on the database
            {
                if (acc.Email == email)         //If an account has the correct email check if the password is also correct
                {
                    errorFlag = 1;
                    if (acc.Password == password)
                        return true;

                    return false;
                }
            }
            errorFlag = 0;
            return false;
        }

        public bool AvailableEmailAddress(string email)
        {
            foreach(Account acc in Users)
            {
                if (acc.Email == email)
                    return false;
            }

            return true;
        }

        public void AddAccountToSystem(int age, string name, string email, string password)
        {
            Users.Add(new Account(age, name, email, password));
        }

        private void CreateBookCollection()     //TODO: To be replaced by database eventually
        {
            BookCollection.Add(new Book("Of Mice and Men", "John Steinbeck", 4, new DateTime(1937, 11, 23)));
            BookCollection.Add(new Book("To Kill a Mockingbird", "Harper Lee", 1, new DateTime(1960, 7, 11)));
            BookCollection.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", 4, new DateTime(1925, 4, 10)));
            BookCollection.Add(new Book("The Invisible Man", "H. G. Wells", 1, new DateTime(1897, 1, 1)));
            BookCollection.Add(new Book("Beloved", "Toni Morrison", 3, new DateTime(1987, 9, 1)));
            BookCollection.Add(new Book("The Hobbit", "J. R. R. Tolkien", 3, new DateTime(1937, 1, 1)));
            BookCollection.Add(new Book("Harry Potter and the Philosopher's Stone", "J. K. Rowling", 5, new DateTime(1997, 6, 26)));
            BookCollection.Add(new Book("The Lion, the Witch and the Wardrobe", "C. S. Lewis", 6, new DateTime(1950, 10, 16)));
            BookCollection.Add(new Book("Harry Potter and the Chamber of Secrets", "J. K. Rowling", 2, new DateTime(1998, 6, 2)));
            BookCollection.Add(new Book("Watership Down", "Richard Adams", 4, new DateTime(1972, 11, 1)));
        }

        private List<Account> Users;
        private List<Book> BookCollection = new List<Book>();      //Could move to another class?
    }
}

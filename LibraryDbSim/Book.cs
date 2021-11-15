using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    public enum BookCategories { Action, Drama, Horror, Romance, Fantasy, Fiction, Non_Fiction };

    public class Book
    {
        public Book() { }
        public Book(int id, string n, string a, int s)
        {
            this.bookID = id;
            this.Name = n;
            this.Author = a;
            this.Stock = s;
        }

        //Variables
        public int bookID { get; set; }
        public string Name { get; private set; }
        public int Stock { get; set; }
        public string Author { get; private set; }
        public BookCategories Category { get; private set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

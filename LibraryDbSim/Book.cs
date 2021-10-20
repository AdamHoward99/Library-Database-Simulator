using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    class Book
    {
        public Book() { }
        public Book(string n, string a, int s, DateTime pd)
        {
            this.Name = n;
            this.Author = a;
            this.Stock = s;
            this.PublishDate = pd;
        }

        //Variables
        private string Name;
        private DateTime PublishDate;
        private int Stock;
        private string Author;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    interface IUser
    {
        void AddBookToList(Book book);   //Adds to current rented books list
        void RemoveBookFromList(Book book);
    }
}

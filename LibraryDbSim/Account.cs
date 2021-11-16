using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDbSim
{
    public class Account
    {
        public Account(int id, string name)
        {
            this.AccountID = id;
            this.Name = name;
        }

        //Variables
        public string Name { get; private set; }
        public int AccountID { get; set; }
    }
}

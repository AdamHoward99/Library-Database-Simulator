using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        LibrarySystem lSystem;
        Account thisAccount;        //Easier method of obtaining data from specific account

        public AccountWindow(LibrarySystem lb, string accountEmail)
        {
            InitializeComponent();
            lSystem = lb;

            thisAccount = lb.GetAccount(accountEmail);      //Gets the account

            //Output Account name
            AccNameLbl.Content = $"Welcome {thisAccount.Name}";

            //Output Account current rented books
            foreach (Book b in thisAccount.GetCurrentRentedBooks())
                CurrentRentedBooksListBox.Items.Add($"{b.Name}");
        }
    }
}

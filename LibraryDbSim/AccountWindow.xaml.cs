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

        private void UpdateErrorLabel(string txt)
        {
            ErrorLbl3.Visibility = Visibility.Visible;
            ErrorLbl3.Content = txt;
        }

        private void RentABook(object sender, RoutedEventArgs e)
        {
            //Simulates renting out a book by fetching random book which is not already rented by the user
            Book bookToRent = lSystem.GetRandomBook();

            //Book is currently out of stock
            if(bookToRent.Stock == 0)
            {
                UpdateErrorLabel("Book is currently out of stock");
                return;
            }

            //Check if book is already rented by user (cannot rent out book twice at same time)
            if(thisAccount.GetCurrentRentedBooks().Find(b => b == bookToRent) == null)
            {
                //Reset error label
                ErrorLbl3.Visibility = Visibility.Hidden;

                //Book is not already rented by user
                bookToRent.Stock--;
                thisAccount.AddBookToList(bookToRent);
                CurrentRentedBooksListBox.Items.Add($"{bookToRent.Name}");
                return;
            }

            //Book is already rented by the user
            UpdateErrorLabel("This book is already rented out");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            //Open up login window, passing along all data information
            MainWindow resetWind = new MainWindow(lSystem);     //Keeps information present during runtime, when moving to db, information will be stored and the LibSystem class will not require parameterizing.
            resetWind.Show();

            //Close Window
            this.Close();
        }
    }
}

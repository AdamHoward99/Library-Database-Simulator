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
        public static bool IsAdditionalWindowOpen = false;

        public AccountWindow(LibrarySystem lb, string accountEmail)
        {
            InitializeComponent();
            lSystem = lb;

            thisAccount = lb.GetAccount(accountEmail);      //Gets the account

            //Output Account name
            AccNameLbl.Content = $"Welcome {thisAccount.Name}";

            //Output all rented books by this user
            RentedBooksData.ItemsSource = thisAccount.GetCurrentRentedBooks();
        }

        private void UpdateErrorLabel(string txt)
        {
            ErrorLbl3.Visibility = Visibility.Visible;
            ErrorLbl3.Content = txt;
        }

        private void RentABook(object sender, RoutedEventArgs e)
        {
            //Goto rent book window
            BookList bl = new BookList(lSystem);
            bl.ShowDialog();

            if(BookList.chosenBook != null)     //A book was chosen in the book list window
            {
                //Check if book is already rented by this used, TODO: in future pass account to book list to check this there
                if(thisAccount.GetCurrentRentedBooks().Find(b => b == BookList.chosenBook) == null)
                {
                    //Reset Error Label
                    ErrorLbl3.Visibility = Visibility.Hidden;

                    //Set Rented Date to today
                    BookList.chosenBook.RentDate = DateTime.Now;

                    //Add book to account
                    BookList.chosenBook.Stock--;
                    thisAccount.AddBookToList(BookList.chosenBook);
                    RentedBooksData.Items.Refresh();
                }

                BookList.chosenBook = null;
            }

            return;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            //Open up login window, passing along all data information
            MainWindow resetWind = new MainWindow(lSystem);     //Keeps information present during runtime, when moving to db, information will be stored and the LibSystem class will not require parameterizing.
            resetWind.Show();

            //Close Window
            this.Close();
        }

        private void ReturnBook(object sender, RoutedEventArgs e)
        {
            int removeBookPos = RentedBooksData.SelectedIndex;

            //Make sure an item is selected
            if (removeBookPos == -1)
                return;

            //Add to stock of book
            lSystem.ReturnBook(RentedBooksData.Items[RentedBooksData.SelectedIndex] as Book);

            //Remove book from accounts rented book list
            thisAccount.RemoveBookFromList(removeBookPos);

            //Confirm window yes/no?, deadlines for books?
            RentedBooksData.Items.Refresh();
        }

        private void ChangeAccountSettings(object sender, RoutedEventArgs e)
        {
            if(!MainWindow.IsAdditionalWindowOpen)
            {
                MainWindow.IsAdditionalWindowOpen = true;
                ResetPassword resetPasswordWindow = new ResetPassword(lSystem);
                resetPasswordWindow.Show();
            }
        }
    }
}

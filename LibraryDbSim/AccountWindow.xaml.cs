using System;
using System.Data;
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
using MySqlConnector;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        LibrarySystem lSystem;
        Account thisAccount = new Account();        //Easier method of obtaining data from specific account
        public static bool IsAdditionalWindowOpen = false;

        public AccountWindow(LibrarySystem lb, string accountEmail)
        {
            InitializeComponent();
            lSystem = lb;

            //Get name of user from database
            lSystem.connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT name FROM accounts where (email = @email)", lSystem.connection);
            cmd.Parameters.AddWithValue("@email", accountEmail);
            AccNameLbl.Content = $"Welcome {Convert.ToString(cmd.ExecuteScalar())}";

            //Outputting current rented books by this user
            //Get ID of this account
            cmd.CommandText = "SELECT accID FROM accounts where email = @email";
            thisAccount.AccountID = Convert.ToInt16(cmd.ExecuteScalar());

            //Get all book orders which contain this account id
            cmd.CommandText = "SELECT * FROM rentedbookorders where accID = @accID";
            cmd.Parameters.AddWithValue("@accID", thisAccount.AccountID);
            DataTable ds = new DataTable();
            ds.Load(cmd.ExecuteReader());
            RentedBooksData.ItemsSource = ds.DefaultView;
            lSystem.connection.Close();
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

            //Ensure a book was chosen before opening next window
            if (BookList.chosenBook == null)
                return;

            //Select rent period for book
            SelectRentTime selectRentTime = new SelectRentTime();
            selectRentTime.ShowDialog();

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
                resetPasswordWindow.ShowDialog();
            }
        }

        private void ExtendBookDuration(object sender, RoutedEventArgs e)
        {
            //Check if a book is selected
            if (RentedBooksData.SelectedItem == null)
                return;

            //Open new window to extend date, similar to selectRentTime Window
            ExtendDuration extendDuration = new ExtendDuration(thisAccount.GetCurrentRentedBooks()[RentedBooksData.SelectedIndex]);
            extendDuration.ShowDialog();

            //Obtain new date for book from extend duration window
            RentedBooksData.Items.Refresh();
        }
    }
}

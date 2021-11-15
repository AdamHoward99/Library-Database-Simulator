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
        private DataTable accountBookOrders = new DataTable();      //Stores book orders of current account locally from database

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
            accountBookOrders.Load(cmd.ExecuteReader());
            RentedBooksData.ItemsSource = accountBookOrders.DefaultView;
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

            if (BookList.chosenBook != null)     //A book was chosen in the book list window
            {
                //Check if book is already rented by this used
                lSystem.connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT orderID FROM rentedbookorders where (accID = @accID AND bookName = @bookName)", lSystem.connection);
                cmd.Parameters.AddWithValue("@accID", thisAccount.AccountID);
                cmd.Parameters.AddWithValue("@bookName", BookList.chosenBook.Name);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)      //Account currently is renting this book, cancel order
                {
                    BookList.chosenBook = null;
                    lSystem.connection.Close();
                    return;
                }

                //Account is not already renting the book
                ErrorLbl3.Visibility = Visibility.Hidden;       //Reset error label

                //Set rent date to today
                BookList.chosenBook.RentDate = DateTime.Now;
                reader.Close();

                //Add book order to rentedbookorders table on database
                cmd.CommandText = "INSERT INTO rentedbookorders (accID, bookName, rentDate, returnDate) VALUES(@accID, @bookName, @rentDate, @returnDate)";
                cmd.Parameters.AddWithValue("@rentDate", BookList.chosenBook.RentDate);
                cmd.Parameters.AddWithValue("@returnDate", BookList.chosenBook.ReturnDate);
                cmd.ExecuteNonQuery();

                //Take one away from available stock in bookcollection table
                BookList.chosenBook.Stock--;
                if (BookList.chosenBook.Stock < 0) BookList.chosenBook.Stock = 0;       //Move into get, setter area

                cmd.CommandText = "UPDATE bookcollection SET bookStock = @bookStock WHERE bookName = @bookName";
                cmd.Parameters.AddWithValue("@bookStock", BookList.chosenBook.Stock);
                cmd.ExecuteNonQuery();

                //Update datatable to reflect new book
                //Add new row for new order
                DataRow newOrderRow = accountBookOrders.NewRow();
                newOrderRow["accID"] = thisAccount.AccountID;
                newOrderRow["bookName"] = BookList.chosenBook.Name;
                newOrderRow["rentDate"] = BookList.chosenBook.RentDate;
                newOrderRow["returnDate"] = BookList.chosenBook.ReturnDate;
                accountBookOrders.Rows.Add(newOrderRow);

                BookList.chosenBook = null;     //Reset local copy
                lSystem.connection.Close();
            }
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
            if (RentedBooksData.SelectedIndex < 0)
                return;

            //Get selected rows information and pass to duration window
            DataRow selectedRow = accountBookOrders.Rows[RentedBooksData.SelectedIndex];
            ExtendDuration extendDuration = new ExtendDuration(selectedRow, lSystem);
            extendDuration.ShowDialog();
        }
    }
}

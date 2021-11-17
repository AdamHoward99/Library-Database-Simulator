using System;
using System.Data;
using System.Windows;

namespace LibraryDbSim
{
    public partial class AccountWindow : Window
    {
        Account thisAccount;        //Easier method of obtaining data from specific account
        public static bool IsAdditionalWindowOpen = false;
        private DataTable accountBookOrders = new DataTable();      //Stores book orders of current account locally from database

        public AccountWindow(string accountEmail)
        {
            InitializeComponent();
            GetAccountInformation(accountEmail);

            //Set Name of the current account user on the label
            AccNameLbl.Content = $"Welcome {thisAccount.Name}";
            GetBookOrders();
        }

        private void UpdateErrorLabel(string txt)
        {
            ErrorLbl3.Visibility = Visibility.Visible;
            ErrorLbl3.Content = txt;
        }

        private void RentABook(object sender, RoutedEventArgs e)
        {
            //Goto rent book window
            BookList bl = new BookList();
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
                DatabaseConnection.conn.Open();
                DatabaseConnection.cmd.CommandText = "SELECT orderID FROM rentedbookorders where (accID = @accID AND bookName = @bookName)";
                DatabaseConnection.cmd.Parameters.AddWithValue("@accID", thisAccount.AccountID);
                DatabaseConnection.cmd.Parameters.AddWithValue("@bookName", BookList.chosenBook.Name);
                DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();

                if (DatabaseConnection.reader.HasRows)      //Account currently is renting this book, cancel order
                {
                    BookList.chosenBook = null;
                    DatabaseConnection.CloseAll();
                    return;
                }

                //Account is not already renting the book
                ErrorLbl3.Visibility = Visibility.Hidden;       //Reset error label

                //Set rent date to today
                BookList.chosenBook.RentDate = DateTime.Now;
                DatabaseConnection.reader.Close();

                AddBookOrder();

                //Update datatable to reflect new book
                UpdateUI();

                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.conn.Close();
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            //Open up login window, passing along all data information
            MainWindow resetWind = new MainWindow();
            resetWind.Show();

            //Close Window
            this.Close();
        }

        private void ReturnBook(object sender, RoutedEventArgs e)
        {
            //Check if a book is selected
            if (RentedBooksData.SelectedIndex < 0)
                return;

            //Get selected rows information and pass to duration window
            DataRow selectedRow = accountBookOrders.Rows[RentedBooksData.SelectedIndex];

            //Add to the stock of the book
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "UPDATE bookcollection SET bookStock = bookStock + 1 WHERE bookName = @bookName";
            DatabaseConnection.cmd.Parameters.AddWithValue("@bookName", selectedRow["bookName"]);
            DatabaseConnection.cmd.ExecuteNonQuery();

            //Remove order from rentedbookorders table on db
            DatabaseConnection.cmd.CommandText = "DELETE from rentedbookorders WHERE bookName = @bookName AND accID = @accID";
            DatabaseConnection.cmd.Parameters.AddWithValue("@accID", thisAccount.AccountID);
            DatabaseConnection.cmd.ExecuteNonQuery();
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.conn.Close();

            //Update UI
            accountBookOrders.Rows.Remove(selectedRow);
        }

        private void ChangeAccountSettings(object sender, RoutedEventArgs e)
        {
            if(!MainWindow.IsAdditionalWindowOpen)
            {
                MainWindow.IsAdditionalWindowOpen = true;
                ResetPassword resetPasswordWindow = new ResetPassword();
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
            ExtendDuration extendDuration = new ExtendDuration(selectedRow);
            extendDuration.ShowDialog();
        }

        private void GetAccountInformation(string email)
        {
            //Get name of user and accID from database
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "SELECT accId, name FROM accounts WHERE email = @email";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();

            if (DatabaseConnection.reader.HasRows)       //Ensure data was obtained from the SELECT command
            {
                DatabaseConnection.reader.Read();

                //Locally store information for future use
                thisAccount = new Account(Convert.ToInt16(DatabaseConnection.reader.GetValue(0)), DatabaseConnection.reader.GetValue(1).ToString());
            }

            DatabaseConnection.reader.Close();
        }

        private void GetBookOrders()
        {
            //Get all book orders which contain this account id
            DatabaseConnection.cmd.CommandText = "SELECT * FROM rentedbookorders where accID = @accID";
            DatabaseConnection.cmd.Parameters.AddWithValue("@accID", thisAccount.AccountID);
            accountBookOrders.Load(DatabaseConnection.cmd.ExecuteReader());
            RentedBooksData.ItemsSource = accountBookOrders.DefaultView;
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.conn.Close();
        }

        private void UpdateUI()
        {
            //Add new row for new order
            DataRow newOrderRow = accountBookOrders.NewRow();
            newOrderRow["accID"] = thisAccount.AccountID;
            newOrderRow["bookName"] = BookList.chosenBook.Name;
            newOrderRow["rentDate"] = BookList.chosenBook.RentDate;
            newOrderRow["returnDate"] = BookList.chosenBook.ReturnDate;
            accountBookOrders.Rows.Add(newOrderRow);
            BookList.chosenBook = null;     //Reset local copy
        }

        private void AddBookOrder()
        {
            //Add book order to rentedbookorders table on database
            DatabaseConnection.cmd.CommandText = "INSERT INTO rentedbookorders (accID, bookName, rentDate, returnDate) VALUES(@accID, @bookName, @rentDate, @returnDate)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@rentDate", BookList.chosenBook.RentDate);
            DatabaseConnection.cmd.Parameters.AddWithValue("@returnDate", BookList.chosenBook.ReturnDate);
            DatabaseConnection.cmd.ExecuteNonQuery();

            //Take one away from available stock in bookcollection table
            BookList.chosenBook.Stock--;
            if (BookList.chosenBook.Stock < 0) BookList.chosenBook.Stock = 0;

            DatabaseConnection.cmd.CommandText = "UPDATE bookcollection SET bookStock = @bookStock WHERE bookName = @bookName";
            DatabaseConnection.cmd.Parameters.AddWithValue("@bookStock", BookList.chosenBook.Stock);
            DatabaseConnection.cmd.ExecuteNonQuery();
        }
    }
}

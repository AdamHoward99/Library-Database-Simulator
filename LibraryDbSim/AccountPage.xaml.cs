using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace LibraryDbSim
{
    public partial class AccountPage : Page
    {
        public static Account thisAccount;        //Easier method of obtaining data from specific account
        public static bool IsAdditionalWindowOpen = false;
        private DataTable accountBookOrders = new DataTable();      //Stores book orders of current account locally from database

        public AccountPage()        //Constructor for when returning from selectRentTimePage
        {
            InitializeComponent();

            //Set Name of the current account user on the label
            AccNameLbl.Content = $"Welcome {thisAccount.Name}";
            GetBookOrders();
        }

        public AccountPage(string accountEmail)
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
            //Goto rent book page
            NavigationService.Navigate(new Uri("BookListPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            //Returns back to login page after logging out of account
            NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ReturnBook(object sender, RoutedEventArgs e)
        {
            //Check if a book is selected
            if (RentedBooksData.SelectedIndex < 0)
                return;

            //Get selected rows information and pass to duration window
            DataRow selectedRow = accountBookOrders.Rows[RentedBooksData.SelectedIndex];

            //Add to the stock of the book
            if (!DatabaseConnection.TryConnection())
                return;

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
            NavigationService.Navigate(new Uri("ResetPasswordPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ExtendBookDuration(object sender, RoutedEventArgs e)
        {
            //Check if a book is selected
            if (RentedBooksData.SelectedIndex < 0)
                return;

            //Get selected rows information and pass to duration window
            DataRow selectedRow = accountBookOrders.Rows[RentedBooksData.SelectedIndex];
            ExtendDurationPage page = new ExtendDurationPage(selectedRow);
            NavigationService.Navigate(page);
        }

        private void GetAccountInformation(string email)
        {
            //Get name of user and accID from database
            if (!DatabaseConnection.TryConnection())
                return;

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
            DatabaseConnection.conn.Close();
        }

        private void GetBookOrders()
        {
            //Get all book orders which contain this account id
            if (!DatabaseConnection.TryConnection())
                return;

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
            newOrderRow["bookName"] = BookListPage.chosenBook.Name;
            newOrderRow["rentDate"] = BookListPage.chosenBook.RentDate;
            newOrderRow["returnDate"] = BookListPage.chosenBook.ReturnDate;
            accountBookOrders.Rows.Add(newOrderRow);
            BookListPage.chosenBook = null;     //Reset local copy
        }
    }
}

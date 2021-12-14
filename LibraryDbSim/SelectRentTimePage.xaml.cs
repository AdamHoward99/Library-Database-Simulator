using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LibraryDbSim
{
    public partial class SelectRentTimePage : Page
    {
        public SelectRentTimePage()
        {
            InitializeComponent();

            //Remove Dates that are not available for choosing
            ReturnDatePicker.DisplayDateStart = DateTime.Now;
            ReturnDatePicker.DisplayDateEnd = DateTime.Now.AddDays(21);
        }

        private void ConfirmDate(object sender, RoutedEventArgs e)
        {
            if (ReturnDatePicker.SelectedDate == null)
                return;

            //Add rent date to chosen book to be added into account
            BookListPage.chosenBook.ReturnDate = ReturnDatePicker.SelectedDate.Value;

            //Check if book is already rented by this used
            if (!DatabaseConnection.TryConnection())
                return;

            DatabaseConnection.cmd.CommandText = "SELECT orderID FROM rentedbookorders where (accID = @accID AND bookName = @bookName)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@accID", AccountPage.thisAccount.AccountID);
            DatabaseConnection.cmd.Parameters.AddWithValue("@bookName", BookListPage.chosenBook.Name);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();

            if (DatabaseConnection.reader.HasRows)      //Account currently is renting this book, cancel order
            {
                BookListPage.chosenBook = null;
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.CloseAll();
                NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.RelativeOrAbsolute));
                return;
            }

            //Set rent date to today
            BookListPage.chosenBook.RentDate = DateTime.Now;
            DatabaseConnection.reader.Close();

            AddBookOrder();

            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.conn.Close();

            NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Cancel(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.RelativeOrAbsolute));

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Checks if closes prematurely, if so cancel the rent process
            if (BookListPage.chosenBook.ReturnDate.Year == 0001)
                BookListPage.chosenBook = null;
        }

        private void AddBookOrder()
        {
            //Add book order to rentedbookorders table on database
            DatabaseConnection.cmd.CommandText = "INSERT INTO rentedbookorders (accID, bookName, rentDate, returnDate) VALUES(@accID, @bookName, @rentDate, @returnDate)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@rentDate", BookListPage.chosenBook.RentDate);
            DatabaseConnection.cmd.Parameters.AddWithValue("@returnDate", BookListPage.chosenBook.ReturnDate);
            DatabaseConnection.cmd.ExecuteNonQuery();

            //Take one away from available stock in bookcollection table
            BookListPage.chosenBook.Stock--;
            if (BookListPage.chosenBook.Stock < 0) BookListPage.chosenBook.Stock = 0;

            DatabaseConnection.cmd.CommandText = "UPDATE bookcollection SET bookStock = @bookStock WHERE bookName = @bookName";
            DatabaseConnection.cmd.Parameters.AddWithValue("@bookStock", BookListPage.chosenBook.Stock);
            DatabaseConnection.cmd.ExecuteNonQuery();
        }
    }
}

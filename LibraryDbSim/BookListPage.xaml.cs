using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LibraryDbSim
{
    public partial class BookListPage : Page
    {
        public static Book chosenBook;      //Book chosen to rent, is used by AccountWindow to add book to account rented books list
        DataTable bookListData = new DataTable();       //Stores available books from bookcollection table on database

        public BookListPage()
        {
            InitializeComponent();

            //Get All available books on system (stock > 0) from database
            if (!DatabaseConnection.TryConnection())
                return;

            DatabaseConnection.cmd.CommandText = "SELECT * FROM bookcollection where (bookStock > 0)";
            bookListData.Load(DatabaseConnection.cmd.ExecuteReader());
            dataGrid.ItemsSource = bookListData.DefaultView;
            DatabaseConnection.conn.Close();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //An item from the datagrid is selected
            if (dataGrid.SelectedIndex != -1)
            {
                //Get selected book information from datatable and store in chosenBook variable
                DataRow selectedRow = bookListData.Rows[dataGrid.SelectedIndex];
                chosenBook = new Book(Convert.ToInt16(selectedRow["bookID"]), selectedRow["bookName"].ToString(), selectedRow["bookAuthor"].ToString(), Convert.ToInt16(selectedRow["bookStock"]));

                //Navigate to next page (SelectRentTimePage)
                NavigationService.Navigate(new Uri("SelectRentTimePage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}

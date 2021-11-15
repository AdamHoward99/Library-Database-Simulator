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
using System.Data;
using MySqlConnector;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for BookList.xaml
    /// </summary>
    public partial class BookList : Window
    {
        LibrarySystem lSystem;      //Is Passed from account window, only requires book collection list so if that moves, remove this TODO
        public static Book chosenBook;      //Book chosen to rent, is used by AccountWindow to add book to account rented books list
        DataTable bookListData = new DataTable();       //Stores available books from bookcollection table on database

        public BookList(LibrarySystem lb)
        {
            lSystem = lb;
            InitializeComponent();

            //Get All available books on system (stock > 0) from database
            lSystem.connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM bookcollection where (bookStock > 0)", lSystem.connection);
            bookListData.Load(cmd.ExecuteReader());
            dataGrid.ItemsSource = bookListData.DefaultView;
            lSystem.connection.Close();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //An item from the datagrid is selected
            if(dataGrid.SelectedIndex != -1)
            {
                //Get selected book information from datatable and store in chosenBook variable
                DataRow selectedRow = bookListData.Rows[dataGrid.SelectedIndex];
                chosenBook = new Book(Convert.ToInt16(selectedRow["bookID"]), selectedRow["bookName"].ToString(), selectedRow["bookAuthor"].ToString(), Convert.ToInt16(selectedRow["bookStock"]));
            }

            this.Close();
        }
    }
}

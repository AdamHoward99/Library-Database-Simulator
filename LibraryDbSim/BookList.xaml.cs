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

        public BookList(LibrarySystem lb)
        {
            lSystem = lb;
            InitializeComponent();

            //Function to list all available books (stock > 0)
            var availableBooks = from b in lSystem.GetBookCollection() where b.Stock > 0 select b;
            dataGrid.ItemsSource = availableBooks;


            //Search bar in future?
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //An item from the datagrid is selected
            if(dataGrid.SelectedIndex != -1)
                chosenBook = dataGrid.Items[dataGrid.SelectedIndex] as Book;        //Stores book chosen to be added to account in accountwindow

            this.Close();
        }
    }
}

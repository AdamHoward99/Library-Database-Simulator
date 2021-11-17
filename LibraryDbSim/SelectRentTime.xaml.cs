using System;
using System.Windows;

namespace LibraryDbSim
{
    public partial class SelectRentTime : Window
    {
        public SelectRentTime()
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
            BookList.chosenBook.ReturnDate = ReturnDatePicker.SelectedDate.Value;

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e) => this.Close();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Checks if closes prematurely, if so cancel the rent process
            if (BookList.chosenBook.ReturnDate.Year == 0001)
                BookList.chosenBook = null;
        }
    }
}

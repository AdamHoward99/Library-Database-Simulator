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
    public partial class SelectRentTime : Window
    {
        public SelectRentTime()
        {
            InitializeComponent();

            //Blackout dates on the date picker which are in the past or beyond 2 weeks from today

            //TODO: choose a method to use

            //Blackout Method
            //ReturnDatePicker.BlackoutDates.AddDatesInPast();
            //ReturnDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(14), DateTime.Now.AddYears(1)));

            //Remove Method
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

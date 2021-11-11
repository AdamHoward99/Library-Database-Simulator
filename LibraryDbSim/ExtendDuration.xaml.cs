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
    public partial class ExtendDuration : Window
    {
        Book book;

        public ExtendDuration(Book chosenBook)
        {
            InitializeComponent();

            book = chosenBook;

            //Blackout dates on the date picker which are in the past or beyond 2 weeks from today
            ExtendDatePicker.DisplayDateStart = DateTime.Now;
            ExtendDatePicker.DisplayDateEnd = DateTime.Now.AddDays(21);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) => this.Close();

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ExtendDatePicker.SelectedDate == null)
                return;

            book.ReturnDate = ExtendDatePicker.SelectedDate.Value;
            this.Close();
        }
    }
}

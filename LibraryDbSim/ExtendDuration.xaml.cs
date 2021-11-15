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
using System.Data;

namespace LibraryDbSim
{
    public partial class ExtendDuration : Window
    {
        DataRow selectedItemRow;     //Used to find orderID on the database
        LibrarySystem lsystem;      //Only passed to use the mysql connection, TODO: change this in future

        public ExtendDuration(DataRow row, LibrarySystem ls)
        {
            InitializeComponent();

            selectedItemRow = row;
            lsystem = ls;

            //Blackout dates on the date picker which are in the past or beyond 2 weeks from today
            ExtendDatePicker.DisplayDateStart = DateTime.Now;
            ExtendDatePicker.DisplayDateEnd = DateTime.Now.AddDays(21);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) => this.Close();

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ExtendDatePicker.SelectedDate == null)
                return;

            //Update book order with new date in rentedbookorder table on db
            lsystem.connection.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE rentedbookorders SET returnDate = @returnDate WHERE orderID = @orderID", lsystem.connection);
            cmd.Parameters.AddWithValue("@returnDate", ExtendDatePicker.SelectedDate.Value);
            cmd.Parameters.AddWithValue("@orderID", selectedItemRow["orderID"]);
            cmd.ExecuteNonQuery();
            lsystem.connection.Close();

            //Update UI of app
            selectedItemRow["returnDate"] = ExtendDatePicker.SelectedDate.Value;
            this.Close();
        }
    }
}

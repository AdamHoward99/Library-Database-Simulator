using System;
using System.Windows;
using System.Data;

namespace LibraryDbSim
{
    public partial class ExtendDuration : Window
    {
        DataRow selectedItemRow;     //Used to find orderID on the database

        public ExtendDuration(DataRow row)
        {
            InitializeComponent();

            selectedItemRow = row;

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
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "UPDATE rentedbookorders SET returnDate = @returnDate WHERE orderID = @orderID";
            DatabaseConnection.cmd.Parameters.AddWithValue("@returnDate", ExtendDatePicker.SelectedDate.Value);
            DatabaseConnection.cmd.Parameters.AddWithValue("@orderID", selectedItemRow["orderID"]);
            DatabaseConnection.cmd.ExecuteNonQuery();
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.conn.Close();

            //Update UI of app
            selectedItemRow["returnDate"] = ExtendDatePicker.SelectedDate.Value;
            this.Close();
        }
    }
}

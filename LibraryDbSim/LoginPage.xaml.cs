﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace LibraryDbSim
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Ensure username/password textboxes are not empty before validating
            if (string.IsNullOrWhiteSpace(UsernameTxtBox.Text) && string.IsNullOrWhiteSpace(PasswordTxtBox.Password))
            {
                UpdateErrorLabel("No data has been entered!");
                return;
            }

            //Validate Account details
            if (AccountValid(UsernameTxtBox.Text, DatabaseConnection.EncryptTextToCipher(PasswordTxtBox.Password), out int error))
            {
                //TODO: get windowFrame var from MainWindow xaml, can do it using below, or declare a static var to windowFrame?
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("TestPage.xaml", UriKind.RelativeOrAbsolute));
                

                //windowFrame.Content = null;     //Clear the last page to prepare for the next frame page
                //windowFrame.Navigate(new Uri("TestPage.xaml", UriKind.RelativeOrAbsolute));

                //AccountWindow accWind = new AccountWindow(UsernameTxtBox.Text);     //Pass data on before closing
                //accWind.Show();
                //this.Close();       //TODO IF PAGES ARE BETTER THAN WINDOWS?
                return;
            }

            //Output correct error based on returning error value from AccountValid function
            UpdateErrorLabel(error == 0 ? "Entered email is incorrect!" : "Entered password is incorrect!");
            ForgotPasswordLbl.Visibility = Visibility.Visible;
        }

        private void UpdateErrorLabel(string errorMsg)
        {
            ErrorLbl.Visibility = Visibility.Visible;
            ErrorLbl.Content = errorMsg;
        }

        private void SignUpLbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("CreateAccountPage.xaml", UriKind.RelativeOrAbsolute));
            //CreateAccount createAccWindow = new CreateAccount();
            //createAccWindow.ShowDialog();
        }

        private void ForgotPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetPassword resetPasswordWindow = new ResetPassword();
            resetPasswordWindow.ShowDialog();
        }

        private bool AccountValid(string email, string password, out int errorFlag)
        {
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "SELECT accID FROM accounts WHERE (email = @email AND password = @password)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.cmd.Parameters.AddWithValue("@password", password);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();
            if (DatabaseConnection.reader.HasRows)     //Account found that matches information, account is valid
            {
                errorFlag = 1;
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.CloseAll();
                return true;
            }

            errorFlag = 0;
            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.CloseAll();
            return false;
        }
    }
}

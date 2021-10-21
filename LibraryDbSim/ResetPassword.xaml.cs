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
using System.Windows.Shapes;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        LibrarySystem lSystem;

        public ResetPassword(LibrarySystem ldb)
        {
            InitializeComponent();
            lSystem = ldb;
        }

        private void ResetPasswordWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.IsAdditionalWindowOpen = false;
        }

        private void ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            //Validate Entered values
            if(string.IsNullOrWhiteSpace(EmailResTxtBox.Text) || string.IsNullOrWhiteSpace(NewPasswordBox.Password) || string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                Console.WriteLine("Please enter all information");
                return;
            }

            if(lSystem.AvailableEmailAddress(EmailResTxtBox.Text))     //Email is not contained in the user database
            {
                Console.WriteLine("Email not found");
                return;
            }

            if(NewPasswordBox.Password != ConfirmPasswordBox.Password)      //Password and confirm password are different values
            {
                Console.WriteLine("Please confirm the same password");
                return;
            }

            if(!lSystem.CheckNewUserPassword(EmailResTxtBox.Text, NewPasswordBox.Password))     //Entered password is same as current
            {
                Console.WriteLine("Password is same as current");
                return;
            }

            //Validation Complete
            this.Close();
        }
    }
}
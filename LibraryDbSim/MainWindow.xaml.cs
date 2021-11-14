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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Theming;
using MySqlConnector;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create Library System
        LibrarySystem lSystem = new LibrarySystem();        //TODO: Store this data on a db, until then pass this class to all windows

        //Variable to prevent multiple other windows opening, max amount of windows is this + 1
        public static bool IsAdditionalWindowOpen = false;

        public MainWindow()     //Initial Constructor used for initial startup of application
        {
            InitializeComponent();
        }

        public MainWindow(LibrarySystem lb)     //Constructor used when returning to this window from the account window
        {
            InitializeComponent();
            lSystem = lb;           //Saves any changes made
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: In future check email with database instead of stored credentials

            //Ensure username/password textboxes are not empty before validating
            if(string.IsNullOrWhiteSpace(UsernameTxtBox.Text) && string.IsNullOrWhiteSpace(PasswordTxtBox.Password))
            {
                UpdateErrorLabel("No data has been entered!");
                return;
            }

            //Validate Account details
            if (lSystem.AccountValid(UsernameTxtBox.Text, PasswordTxtBox.Password, out int error))
            {
                AccountWindow accWind = new AccountWindow(lSystem, UsernameTxtBox.Text);     //Pass data on before closing
                accWind.Show();
                this.Close();
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
            if(!IsAdditionalWindowOpen)
            {
                IsAdditionalWindowOpen = true;

                //Open new window for user to sign up
                CreateAccount createAccWindow = new CreateAccount(lSystem);
                createAccWindow.ShowDialog();
            }
        }

        private void ForgotPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(!IsAdditionalWindowOpen)
            {
                IsAdditionalWindowOpen = true;
                ResetPassword resetPasswordWindow = new ResetPassword(lSystem);
                resetPasswordWindow.ShowDialog();
            }
        }
    }
}

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

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create Library System
        LibrarySystem lSystem = new LibrarySystem();

        //Variable to prevent multiple Create Account Windows opening
        private bool IsCreateAccWindowOpen = false;

        public MainWindow()
        {
            InitializeComponent();

            //Add Accounts, TODO: Get Accounts from database in the future
            lSystem.AddDecoyAccounts();
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
                Console.WriteLine("Account is on the system");      //TODO: Open new window for account
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
            if(!IsCreateAccWindowOpen)
            {
                IsCreateAccWindowOpen = true;

                //Open new window for user to sign up
                CreateAccount createAccWindow = new CreateAccount();
                createAccWindow.Show();
            }
        }

        private void ForgotPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Open window to change password");
        }

        private void SignUpLbl_MouseEnter(object sender, MouseEventArgs e)
        {
            //SignUpLbl.Foreground = new SolidColorBrush(Colors.Gray);
            //Make text underlined when hovering over?
            Console.WriteLine("It has entered the area");
        }
    }
}

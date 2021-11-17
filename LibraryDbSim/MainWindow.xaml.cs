using System.Windows;
using System.Windows.Input;

namespace LibraryDbSim
{
    public partial class MainWindow : Window
    {
        //Variable to prevent multiple other windows opening, max amount of windows is this + 1
        public static bool IsAdditionalWindowOpen = false;

        public MainWindow()     //Initial Constructor used for initial startup of application
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Ensure username/password textboxes are not empty before validating
            if(string.IsNullOrWhiteSpace(UsernameTxtBox.Text) && string.IsNullOrWhiteSpace(PasswordTxtBox.Password))
            {
                UpdateErrorLabel("No data has been entered!");
                return;
            }

            //Validate Account details
            if (AccountValid(UsernameTxtBox.Text, PasswordTxtBox.Password, out int error))
            {
                AccountWindow accWind = new AccountWindow(UsernameTxtBox.Text);     //Pass data on before closing
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
                CreateAccount createAccWindow = new CreateAccount();
                createAccWindow.ShowDialog();
            }
        }

        private void ForgotPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(!IsAdditionalWindowOpen)
            {
                IsAdditionalWindowOpen = true;
                ResetPassword resetPasswordWindow = new ResetPassword();
                resetPasswordWindow.ShowDialog();
            }
        }

        private bool AccountValid(string email, string password, out int errorFlag)
        {
            DatabaseConnection.conn.Open();
            DatabaseConnection.cmd.CommandText = "SELECT * FROM accounts WHERE (email = @email AND password = @password)";
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

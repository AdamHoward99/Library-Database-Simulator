using System.Windows;

namespace LibraryDbSim
{
    public partial class ResetPassword : Window
    {
        LibrarySystem lSystem = new LibrarySystem();

        public ResetPassword()
        {
            InitializeComponent();
        }

        private void ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            //Validate Entered values
            if(string.IsNullOrWhiteSpace(EmailResTxtBox.Text) || string.IsNullOrWhiteSpace(NewPasswordBox.Password) || string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                UpdateErrorLabel("Please enter all information!");
                return;
            }

            if(lSystem.AvailableEmailAddress(EmailResTxtBox.Text))     //Email is not contained in the user database
            {
                UpdateErrorLabel("Email not found");
                return;
            }

            if(NewPasswordBox.Password != ConfirmPasswordBox.Password)      //Password and confirm password are different values
            {
                UpdateErrorLabel("Passwords do not match");
                return;
            }

            if(!CheckNewUserPassword(EmailResTxtBox.Text, DatabaseConnection.EncryptTextToCipher(NewPasswordBox.Password)))     //Entered password is same as current
            {
                UpdateErrorLabel("Password is same as current");
                return;
            }

            //Validation Complete
            this.Close();
        }

        private void UpdateErrorLabel(string txt)
        {
            ErrorLbl3.Visibility = Visibility.Visible;
            ErrorLbl3.Content = txt;
        }

        private bool CheckNewUserPassword(string email, string newPassword)
        {
            DatabaseConnection.conn.Open();

            //Find user with selected email and make sure password is different
            DatabaseConnection.cmd.CommandText = "SELECT * FROM accounts WHERE (email = @email AND password != @password)";
            DatabaseConnection.cmd.Parameters.AddWithValue("@email", email);
            DatabaseConnection.cmd.Parameters.AddWithValue("@password", newPassword);
            DatabaseConnection.reader = DatabaseConnection.cmd.ExecuteReader();
            if (DatabaseConnection.reader.HasRows)      //Account email has been found with a different password, reset the password
            {
                //Change the password
                DatabaseConnection.reader.Close();
                DatabaseConnection.cmd.CommandText = "UPDATE accounts SET password = @password WHERE email = @email";
                DatabaseConnection.cmd.ExecuteNonQuery();
                DatabaseConnection.cmd.Parameters.Clear();
                DatabaseConnection.conn.Close();
                return true;
            }

            DatabaseConnection.cmd.Parameters.Clear();
            DatabaseConnection.CloseAll();
            return false;
        }
    }
}

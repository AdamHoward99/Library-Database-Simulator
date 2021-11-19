using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace LibraryDbSim
{
    public partial class CreateAccount : Window
    {
        LibrarySystem lSystem = new LibrarySystem();

        public CreateAccount()
        {
            InitializeComponent();
        }

        //Variables
        private readonly Regex NumbersOnlyRegex = new Regex("[^0-9]+");
        private readonly Regex LettersOnlyRegex = new Regex("[a-zA-Z]");

        private bool IsTextNumberOnly(string txt)
        {   return NumbersOnlyRegex.IsMatch(txt); }

        private bool IsTextLettersOnly(string txt)
        { return LettersOnlyRegex.IsMatch(txt); }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            //Validate entered information
            if(!string.IsNullOrWhiteSpace(NameTxtBox.Text) && !string.IsNullOrWhiteSpace(AgeTxtBox.Text) && EmailAccTxtBox.Text.Contains('@') && EmailAccTxtBox.Text.Length > 6
                && !string.IsNullOrWhiteSpace(AccPasswordTxtBox.Password))
            {
                //A name and age has been entered, check if email is free to use
                if(lSystem.AvailableEmailAddress(EmailAccTxtBox.Text))
                {
                    lSystem.AddAccountToSystem(Convert.ToInt16(AgeTxtBox.Text), NameTxtBox.Text, EmailAccTxtBox.Text, AccPasswordTxtBox.Password);
                    this.Close();
                    return;
                }

                //Email already contained on db
                UpdateErrorLbl("Email already taken!");
                return;
            }

            UpdateErrorLbl("Please enter all information!");
        }

        //Only used for textboxes which require only letters
        private void LetterTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = !IsTextLettersOnly(e.Text);

        //Only used for textboxes which require only numbers
        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = IsTextNumberOnly(e.Text);

        private void LetterTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))        //Pasted values are of string type, ignore otherwise
            {
                if (!IsTextLettersOnly((String)e.DataObject.GetData(typeof(String))))         //Prevents numbers being pasted
                    e.CancelCommand();

                return;
            }

            e.CancelCommand();      //Pasted data is not string, ignore
        }

        private void NumberTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))        //Pasted values are of string type, ignore otherwise
            {
                if (IsTextNumberOnly((String)e.DataObject.GetData(typeof(String))))         //Prevents numbers being pasted
                    e.CancelCommand();

                return;
            }

            e.CancelCommand();      //Pasted data is not string, ignore
        }

        private void CreateAccWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.IsAdditionalWindowOpen = false;
        }

        private void UpdateErrorLbl(string txt)
        {
            ErrorLbl2.Visibility = Visibility.Visible;
            ErrorLbl2.Content = txt;
        }
    }
}

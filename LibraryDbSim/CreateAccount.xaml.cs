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
using System.Text.RegularExpressions;

namespace LibraryDbSim
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
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
            //if information is valid, add account to list
            //else output error message


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

        private void NumberTextBox_Pasting(object sender, DataObjectPastingEventArgs e)     //TODO: Combine above function with this
        {
            if (e.DataObject.GetDataPresent(typeof(String)))        //Pasted values are of string type, ignore otherwise
            {
                if (IsTextNumberOnly((String)e.DataObject.GetData(typeof(String))))         //Prevents numbers being pasted
                    e.CancelCommand();

                return;
            }

            e.CancelCommand();      //Pasted data is not string, ignore
        }
    }
}

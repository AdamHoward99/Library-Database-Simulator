using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Diagnostics;
using System;

namespace LibraryDbSim
{
    public partial class MainWindow : Window
    {
        public MainWindow()     //Initial Constructor used for initial startup of application
        {
            InitializeComponent();

            //Setup initial frame view
            windowFrame.Navigate(new Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

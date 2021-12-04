﻿#pragma checksum "..\..\CreateAccount.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21B6DE4488F628CB1CE0066E941E4930E9AE8803"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LibraryDbSim;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LibraryDbSim {
    
    
    /// <summary>
    /// CreateAccount
    /// </summary>
    public partial class CreateAccount : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SignUpLbl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTxtBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AgeTxtBox;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailAccTxtBox;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox AccPasswordTxtBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SignUpBtn;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\CreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLbl2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LibraryDbSim;component/createaccount.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateAccount.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SignUpLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.NameTxtBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\CreateAccount.xaml"
            this.NameTxtBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.LetterTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 43 "..\..\CreateAccount.xaml"
            this.NameTxtBox.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.LetterTextBox_Pasting));
            
            #line default
            #line hidden
            return;
            case 3:
            this.AgeTxtBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 57 "..\..\CreateAccount.xaml"
            this.AgeTxtBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 58 "..\..\CreateAccount.xaml"
            this.AgeTxtBox.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.NumberTextBox_Pasting));
            
            #line default
            #line hidden
            return;
            case 4:
            this.EmailAccTxtBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AccPasswordTxtBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.SignUpBtn = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\CreateAccount.xaml"
            this.SignUpBtn.Click += new System.Windows.RoutedEventHandler(this.SignUpBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ErrorLbl2 = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

﻿#pragma checksum "..\..\AccountPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0B172DB897B2A426F0A0AD3FDA4822A5530800F2"
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
    /// AccountPage
    /// </summary>
    public partial class AccountPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AccNameLbl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RentedBooksData;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RentBookBtn;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExtendDurationBtn;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReturnBookBtn;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SettingsBtn;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogoutBtn;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\AccountPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLbl3;
        
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
            System.Uri resourceLocater = new System.Uri("/LibraryDbSim;component/accountpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AccountPage.xaml"
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
            this.AccNameLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.RentedBooksData = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.RentBookBtn = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\AccountPage.xaml"
            this.RentBookBtn.Click += new System.Windows.RoutedEventHandler(this.RentABook);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ExtendDurationBtn = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\AccountPage.xaml"
            this.ExtendDurationBtn.Click += new System.Windows.RoutedEventHandler(this.ExtendBookDuration);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ReturnBookBtn = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\AccountPage.xaml"
            this.ReturnBookBtn.Click += new System.Windows.RoutedEventHandler(this.ReturnBook);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SettingsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 123 "..\..\AccountPage.xaml"
            this.SettingsBtn.Click += new System.Windows.RoutedEventHandler(this.ChangeAccountSettings);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LogoutBtn = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\AccountPage.xaml"
            this.LogoutBtn.Click += new System.Windows.RoutedEventHandler(this.Logout);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ErrorLbl3 = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


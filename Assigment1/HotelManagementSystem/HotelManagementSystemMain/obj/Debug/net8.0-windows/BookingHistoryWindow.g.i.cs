﻿#pragma checksum "..\..\..\BookingHistoryWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FA81291CDB615283730EFB5E0C502354D478AB96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HotelManagementSystemMain;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace HotelManagementSystemMain {
    
    
    /// <summary>
    /// BookingHistoryWindow
    /// </summary>
    public partial class BookingHistoryWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbBookingHistory;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvwBooking;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle NavBar;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBookReservation;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMyProfile;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBookHistory;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btLogout;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgLogo;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\BookingHistoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgAvatar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HotelManagementSystemMain;component/bookinghistorywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\BookingHistoryWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbBookingHistory = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lvwBooking = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.NavBar = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.lbName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnBookReservation = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\BookingHistoryWindow.xaml"
            this.btnBookReservation.Click += new System.Windows.RoutedEventHandler(this.btnBookReservation_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnMyProfile = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\BookingHistoryWindow.xaml"
            this.btnMyProfile.Click += new System.Windows.RoutedEventHandler(this.btnMyProfile_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnBookHistory = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.btLogout = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\BookingHistoryWindow.xaml"
            this.btLogout.Click += new System.Windows.RoutedEventHandler(this.btLogout_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.imgLogo = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.ImgAvatar = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


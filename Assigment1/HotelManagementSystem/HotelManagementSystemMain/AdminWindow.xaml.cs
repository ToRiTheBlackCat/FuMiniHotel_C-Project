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

namespace HotelManagementSystemMain
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void btnManageRoomInfo_Click(object sender, RoutedEventArgs e)
        {
            ManageRoomWindow manageRoomWindow = new ManageRoomWindow();
            manageRoomWindow.ShowDialog();
        }

        private void btnManageCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerWindow manageCustomerWindow = new ManageCustomerWindow();
            manageCustomerWindow.ShowDialog();
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            CreateReportWindow createReportWindow = new CreateReportWindow();
            createReportWindow.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }
    }
}
//     WilliamShakespeare@FUMiniHotel.org
//     123@ 

//      admin@FUMiniHotelSystem.com
//      @@abc123@@

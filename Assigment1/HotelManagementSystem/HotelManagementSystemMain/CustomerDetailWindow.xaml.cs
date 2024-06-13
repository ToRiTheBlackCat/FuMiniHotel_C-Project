using HotelManagementSystemClassLibrary.Repository;
using HotelManagementSystemMain.Entities;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for CustomerDetailWindow.xaml
    /// </summary>
    public partial class CustomerDetailWindow : Window
    {
        public Customer SelectedCust { get; set; } = null;
        private readonly UserRepository _userRepo;
        public CustomerDetailWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            _userRepo = new UserRepository(context);
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (SelectedCust != null)
            {
                tbxCustID.Text = SelectedCust.CustomerId.ToString();
                tbxCustFullName.Text = SelectedCust.CustomerFullName.ToString();
                tbxEmailAddress.Text = SelectedCust?.EmailAddress.ToString();
                tbxTelephone.Text = SelectedCust.Telephone.ToString();
                dpcBirthday.Text = SelectedCust.CustomerBirthday.ToString();
                tbxPassword.Text = SelectedCust.Password.ToString();
            }
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbxCustFullName.Text == null)
            {
                MessageBox.Show("Please input Customer FullName.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tbxEmailAddress.Text == null)
            {
                MessageBox.Show("Please input Customer EmailAddress.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tbxPassword.Text == null)
            {
                MessageBox.Show("Please input Customer Password.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(tbxTelephone.Text, out int telephone))
            {
                MessageBox.Show("Please enter a valid number for telephone.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (!dpcBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a BirthDay.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Customer customer = new Customer()
            {
                //CustomerId = int.Parse(tbxCustID.Text),
                CustomerFullName = tbxCustFullName.Text,
                Telephone = tbxTelephone.Text,
                EmailAddress = tbxEmailAddress.Text,
                Password = tbxPassword.Text,    
                CustomerBirthday = DateOnly.FromDateTime(dpcBirthday.SelectedDate.Value),
                CustomerStatus = 1 
            };

             await _userRepo.AddOrUpdateCustInfo(customer);
            MessageBox.Show("Customer information saved successfully.");
            ManageCustomerWindow  manageCustomerWindow = new ManageCustomerWindow();
            this.Close();
            manageCustomerWindow.ShowDialog();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxCustID.Text) || !int.TryParse(tbxCustID.Text, out int custID))
            {
                MessageBox.Show("Please select a  room to delete.", "Invalid Room", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            await _userRepo.DeleteCustInfo(custID);
            MessageBox.Show("Room information delete successfully.");
            ManageCustomerWindow manageCustomerWindow = new ManageCustomerWindow();
            this.Close();
            manageCustomerWindow.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerWindow manageCustomerWindow = new ManageCustomerWindow();
            this.Close();
            manageCustomerWindow.ShowDialog();
        }

       
    }
}

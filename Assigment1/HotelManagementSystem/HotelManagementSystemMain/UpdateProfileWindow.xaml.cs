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
using System.Xml.Linq;

namespace HotelManagementSystemMain
{
    /// <summary>
    /// Interaction logic for UpdateProfileWindow.xaml
    /// </summary>
    public partial class UpdateProfileWindow : Window
    {
        private Customer _customer;
        private UserRepository _userRepo;
        public UpdateProfileWindow(Customer customer)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            InitializeComponent();
            _customer = customer;
            _userRepo = new UserRepository(context);
            FillUserInfo(_customer);
        }
        private void FillUserInfo(Customer cust)
        {
            lbName.Content = cust.CustomerFullName;
            tbxUserEmail.Text = cust.EmailAddress;
            tbxFullName.Text = cust.CustomerFullName;
            tbxPassword.Text = cust.Password;
            tbxTelephone.Text = cust.Telephone;
            // Check if CustomerBirthday has a value
            if (cust.CustomerBirthday.HasValue)
            {
                DateOnly birthday = cust.CustomerBirthday.Value;

                // Construct a DateTime object from the DateOnly values
                DateTime birthdayDateTime = new DateTime(birthday.Year, birthday.Month, birthday.Day);

                // Assign the constructed DateTime to the SelectedDate property
                dpBirthday.SelectedDate = birthdayDateTime;
            }
            else
            {
                // If CustomerBirthday is null, set SelectedDate to null
                dpBirthday.SelectedDate = null;
            }
        }



        private void btnBookReservation_Click(object sender, RoutedEventArgs e)
        {


            MainWindow mainWindow = new MainWindow(_customer.CustomerFullName, _customer.EmailAddress);
            this.Close();
            mainWindow.Show();
        }
        private async void btnBookHistory_Click(object sender, RoutedEventArgs e)
        {
            var userEmail = _customer.EmailAddress;
            var customer = await  _userRepo.GetCustomerByEmail(userEmail);
            BookingHistoryWindow bookingHistoryWindow = new BookingHistoryWindow(customer);
            this.Close();
            bookingHistoryWindow.Show();
        }
        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate email
            if (string.IsNullOrWhiteSpace(tbxUserEmail.Text))
            {
                MessageBox.Show("Please enter an email.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // Validate password
            if (string.IsNullOrWhiteSpace(tbxPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dpBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select birthday date.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate full name
            if (string.IsNullOrWhiteSpace(tbxFullName.Text))
            {
                MessageBox.Show("Please enter your full name.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate telephone number
            if (string.IsNullOrWhiteSpace(tbxTelephone.Text))
            {
                MessageBox.Show("Please enter your telephone number.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Customer customer = new Customer()
            {
                CustomerFullName = tbxFullName.Text,
                EmailAddress = tbxUserEmail.Text,
                Password = tbxPassword.Text,
                Telephone = tbxTelephone.Text,
                CustomerBirthday = DateOnly.FromDateTime(dpBirthday.SelectedDate.Value)
            };
            await _userRepo.UpdateCustomerInfo(customer);
            MessageBox.Show("Update successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FillUserInfo(_customer);
        }

        
    }
}

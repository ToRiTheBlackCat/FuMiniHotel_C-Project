using HotelManagementSystemClassLibrary.Repository;
using HotelManagementSystemMain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagementSystemMain
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserRepository _userRepo;
        public LoginWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            // Set the initial visibility of the grids
            gridLogin.Visibility = Visibility.Visible;
            gridRegister.Visibility = Visibility.Collapsed;

            _userRepo = new UserRepository(context);
        }

        private void ModeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // If slider value is greater than 0.5, show gridRegister, else show gridLogin
            if (ModeSlider.Value > 0.5)
            {
                gridRegister.Visibility = Visibility.Visible;
                gridLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                gridRegister.Visibility = Visibility.Collapsed;
                gridLogin.Visibility = Visibility.Visible;
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Check if email and password fields are empty
            if (string.IsNullOrWhiteSpace(tbxEmail.Text) || string.IsNullOrWhiteSpace(pbxPassword.Password))
            {
                MessageBox.Show("Please enter both email and password.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var email = tbxEmail.Text;
            var pass = new NetworkCredential("", pbxPassword.SecurePassword).Password;
            if (email.Equals("admin@FUMiniHotelSystem.com") && pass.Equals("@@abc123@@"))
            {

                AdminWindow adminWindow = new AdminWindow();
                this.Close();
                adminWindow.Show();
            }
            else
            {
                var cus = await _userRepo.LoginByEmailAndPassword(email, pass);
                if (cus != null)
                {
                    MainWindow mainWindow = new MainWindow(cus.CustomerFullName, cus.EmailAddress);
                    this.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Login failed. No user found.", "Please try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Validate email
            if (string.IsNullOrWhiteSpace(tbxEmail2.Text))
            {
                MessageBox.Show("Please enter an email.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // Validate password
            if (string.IsNullOrWhiteSpace(pbxPassword2.Password))
            {
                MessageBox.Show("Please enter a password.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate confirm password
            if (string.IsNullOrWhiteSpace(pbxConfirmPassword.Password))
            {
                MessageBox.Show("Please confirm your password.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if passwords match
            if (pbxPassword2.Password != pbxConfirmPassword.Password)
            {
                MessageBox.Show("Confirm passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            var cus = await _userRepo.GetCustomerByEmail(tbxEmail2.Text);
            if (cus == null)
            {
                Customer customer = new Customer
                {
                    EmailAddress = tbxEmail2.Text,
                    Password = new NetworkCredential("", pbxPassword2.SecurePassword).Password,
                    CustomerFullName = tbxFullName.Text,
                    Telephone = tbxTelephone.Text,
                    CustomerBirthday = DateOnly.FromDateTime(dpBirthday.SelectedDate.Value),
                    CustomerStatus = 1

                };

                await _userRepo.RegisterAccount(customer);
                MessageBox.Show("Register successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Warning);

                MainWindow mainWindow = new MainWindow(customer.CustomerFullName, customer.EmailAddress);
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Email has been registered.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExist_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using HotelManagementSystemClassLibrary.Repository;
using HotelManagementSystemMain.Entities;
using HotelManagementSystemMain.Models;
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
    /// Interaction logic for BookingHistoryWindow.xaml
    /// </summary>
    public partial class BookingHistoryWindow : Window
    {
        private Customer _customer;
        private UserRepository _userRepo;
        private BookingRepository _bookingRepo;
        public BookingHistoryWindow( Customer customer)
        {
            _customer = customer;
            
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            _bookingRepo = new BookingRepository(context);
            _userRepo = new UserRepository(context);
            InitializeComponent();
            LoadBookingReservations();
            lbName.Content = _customer.CustomerFullName;
        }
        private async void LoadBookingReservations()
        {
            try
            {
                // Retrieve booking reservations for the customer
                List<BookingDetailModel> bookingReservations =  _bookingRepo.GetBookingsForCustomerAsync(_customer);

                // Bind the booking reservations to the ListView
                lvwBooking.ItemsSource = bookingReservations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBookReservation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_customer.CustomerFullName, _customer.EmailAddress);
            this.Close();
            mainWindow.Show();
        }

        private async void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            var customer = await _userRepo.GetCustomerByEmail(_customer.EmailAddress);
            UpdateProfileWindow profileWindow = new UpdateProfileWindow(customer);
            this.Close();
            profileWindow.Show();
        }

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        
    }
}

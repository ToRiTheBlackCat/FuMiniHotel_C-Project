using HotelManagementSystemMain.Entities;
using HotelManagementSystemClassLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;
using System.Windows.Media;

namespace HotelManagementSystemMain
{
    public partial class MainWindow : Window
    {
        private BookingRepository _bookingRepo;
        private UserRepository _userRepo;
        private RoomRepository _roomRepo;
        private Customer _cust = new Customer();

        private List<BookingDetail> _bookingDetails;

        public MainWindow( string userName, string email )
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);

            _bookingRepo = new BookingRepository(context);
            _userRepo = new UserRepository(context);
            _roomRepo = new RoomRepository(context);
            _bookingDetails = new List<BookingDetail>();
            lbName.Content = userName;
            tbxUserEmail.Text = email;
            LoadDataCbo();
            _cust.EmailAddress = email;
            
        }
        //     WilliamShakespeare@FUMiniHotel.org
        //     123@    

        
        private async void LoadDataCbo()
        {
            try
            {
                var roomTypes = await _roomRepo.LoadRoomType();
                cbxRoomType.ItemsSource = roomTypes;    //Đổ data vào 
                cbxRoomType.DisplayMemberPath = nameof(RoomType.RoomTypeName);  //Cái để show ra user thấy 
                cbxRoomType.SelectedValuePath = nameof(RoomType.RoomTypeId);    //Value thật sự đằng sau 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void cbxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxRoomType.SelectedValue != null)
            {
                ClearDetails();
                var selectedRoomType = cbxRoomType.SelectedItem as RoomType;
                if (selectedRoomType != null)
                {
                    tbxRoomTypeDescription.Text = selectedRoomType.TypeDescription;

                    int selectedRoomTypeId = selectedRoomType.RoomTypeId;
                    var roomInfo = await _roomRepo.LoadRoomInfo(selectedRoomTypeId);
                    cbxRoomNumber.DisplayMemberPath = "RoomNumber";
                    cbxRoomNumber.SelectedValuePath = "RoomId";
                    cbxRoomNumber.ItemsSource = roomInfo;
                }
            }
        }

        private void cbxRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxRoomNumber.SelectedItem != null)
            {
                var selectedRoom = cbxRoomNumber.SelectedItem as RoomInformation;
                if (selectedRoom != null)
                {
                    tbxRoomDetailDescription.Text = selectedRoom.RoomDetailDescription;
                    tbxMaxCapacity.Text = selectedRoom.RoomMaxCapacity?.ToString();
                    tbxPricePerDay.Text = selectedRoom.RoomPricePerDay?.ToString("C");
                }
            }
            else
            {
                ClearDetails();
            }
        }

        private void ClearDetails()
        {
            tbxRoomDetailDescription.Text = string.Empty;
            cbxRoomNumber.Text = string.Empty;
            tbxMaxCapacity.Text = string.Empty;
            tbxPricePerDay.Text = string.Empty;
            cbxStartDate.Text = string.Empty;
            cbxEndDate.Text = string.Empty;
            tbxTotalPrice.Text = string.Empty;
        }

        private void cbxStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedStartDate = cbxStartDate.SelectedDate;
            DateTime? selectedEndDate = cbxEndDate.SelectedDate;

            if (selectedStartDate.HasValue && selectedStartDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Start date must be greater than or equal to today.", "Invalid Start Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxStartDate.SelectedDate = null;
            }
            else if (selectedStartDate.HasValue && selectedEndDate.HasValue && selectedStartDate.Value.Date > selectedEndDate.Value.Date)
            {
                MessageBox.Show("Start date cannot be later than end date.", "Invalid Start Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxStartDate.SelectedDate = null;
            }
            else
            {
                CountTotalPrice();
            }
        }

        private void cbxEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedEndDate = cbxEndDate.SelectedDate;
            DateTime? selectedStartDate = cbxStartDate.SelectedDate;

            if (selectedEndDate.HasValue && selectedStartDate.HasValue && selectedEndDate.Value.Date <= selectedStartDate.Value.Date)
            {
                MessageBox.Show("End date must be greater than the start date.", "Invalid End Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                cbxEndDate.SelectedDate = null;
            }
            else
            {
                CountTotalPrice();
            }
        }

        private void CountTotalPrice()
        {
            DateTime? selectedStartDate = cbxStartDate.SelectedDate;
            DateTime? selectedEndDate = cbxEndDate.SelectedDate;

            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                var days = (selectedEndDate.Value.Date - selectedStartDate.Value.Date).Days;
                if (days <= 0)
                {
                    tbxTotalPrice.Text = string.Empty;
                    return;
                }

                if (decimal.TryParse(tbxPricePerDay.Text, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal pricePerDay))
                {
                    var totalPrice = days * pricePerDay;
                    tbxTotalPrice.Text = totalPrice.ToString("C");
                }
                else
                {
                    tbxTotalPrice.Text = string.Empty;
                }
            }
            else
            {
                tbxTotalPrice.Text = string.Empty;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if all required fields are filled
                if (cbxRoomType.SelectedItem == null)
                {
                    MessageBox.Show("Please select a room type.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cbxRoomNumber.SelectedItem == null)
                {
                    MessageBox.Show("Please select a room number.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!cbxStartDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a start date.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!cbxEndDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select an end date.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tbxTotalPrice.Text))
                {
                    MessageBox.Show("Total price is missing. Please ensure all details are filled.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // All fields are filled, proceed with adding booking details
                var bookingDetail = new BookingDetail
                {
                    
                    RoomId = (int)cbxRoomNumber.SelectedValue,
                    StartDate = DateOnly.FromDateTime(cbxStartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(cbxEndDate.SelectedDate.Value),
                    ActualPrice = decimal.Parse(tbxTotalPrice.Text, System.Globalization.NumberStyles.Currency)
                };

                var bookingInfo = new
                {
                    BookingDate = DateTime.Now,
                    RoomType = cbxRoomType.Text,
                    RoomNumber = cbxRoomNumber.Text,
                    StartDate = cbxStartDate.SelectedDate.Value,
                    EndDate = cbxEndDate.SelectedDate.Value,
                    Price = tbxTotalPrice.Text
                };

                _bookingDetails.Add(bookingDetail);
                lvwBooking.Items.Add(bookingInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClearDetails();
        }


        private async void btBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userEmail = tbxUserEmail.Text.Trim();
                var customer = await _userRepo.GetCustomerByEmail(userEmail);
                if (customer == null)
                {
                    MessageBox.Show("Customer not found. Please ensure the email is correct.");
                    return;
                }

                if (!_bookingDetails.Any())
                {
                    // Gather current booking details from the form
                    if (cbxRoomType.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a room type.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (cbxRoomNumber.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a room number.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!cbxStartDate.SelectedDate.HasValue)
                    {
                        MessageBox.Show("Please select a start date.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!cbxEndDate.SelectedDate.HasValue)
                    {
                        MessageBox.Show("Please select an end date.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //if (string.IsNullOrWhiteSpace(tbxTotalPrice.Text))
                    //{
                    //    MessageBox.Show("Total price is missing. Please ensure all details are filled.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}

                    var bookingDetail = new BookingDetail
                    {
                        RoomId = (int)cbxRoomNumber.SelectedValue,
                        StartDate = DateOnly.FromDateTime(cbxStartDate.SelectedDate.Value),
                        EndDate = DateOnly.FromDateTime(cbxEndDate.SelectedDate.Value),
                        ActualPrice = decimal.Parse(tbxTotalPrice.Text, System.Globalization.NumberStyles.Currency)
                    };

                    _bookingDetails.Add(bookingDetail);
                }

                var bookingReservation = new BookingReservation
                {
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    TotalPrice = _bookingDetails.Sum(b => b.ActualPrice),
                    CustomerId = customer.CustomerId,
                    BookingStatus = 1 
                };

                foreach (var detail in _bookingDetails)
                {
                    detail.BookingReservation = bookingReservation;
                    bookingReservation.BookingDetails.Add(detail);
                }

                await _bookingRepo.AddBookingReservation(bookingReservation);

                MessageBox.Show("Booking successful!");
                _bookingDetails.Clear();
                lvwBooking.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            _bookingDetails.Clear();
            lvwBooking.Items.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the clicked button
                var button = sender as Button;

               

                // Get the corresponding booking object from the DataContext of the ListViewItem
                var booking = button.DataContext as BookingDetail;

                // Remove the booking from the _bookingDetails list
                _bookingDetails.Remove(booking);

                // Remove the booking from the ListView
                lvwBooking.Items.Remove(booking);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private async void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            var userEmail = _cust.EmailAddress;
            var customer = await _userRepo.GetCustomerByEmail(userEmail);
            UpdateProfileWindow profileWindow = new UpdateProfileWindow(customer);
            this.Close();
            profileWindow.Show();
        }

        private async void btnBookHistory_Click(object sender, RoutedEventArgs e)
        {
            var userEmail = _cust.EmailAddress;
            var customer = await  _userRepo.GetCustomerByEmail(userEmail);
            BookingHistoryWindow bookingHistoryWindow = new BookingHistoryWindow(customer);
            this.Close();
            bookingHistoryWindow.Show();
        }
    }
}


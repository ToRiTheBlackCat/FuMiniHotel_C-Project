using HotelManagementSystemMain.Entities;
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
    /// Interaction logic for CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {
        public CreateReportWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DateOnly? startDate = dpcStartDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpcStartDate.SelectedDate.Value) : null;
            DateOnly? endDate = dpcEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpcEndDate.SelectedDate.Value) : null;


            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            using (var context = new FuminiHotelManagementContext())
            {
                var reportData = context.BookingDetails
                    .Where(b => b.StartDate >= startDate && b.EndDate <= endDate)
                    .OrderByDescending(b => b.StartDate)
                    .Select(b => new
                    {
                        b.BookingReservationId,
                        b.RoomId,
                        b.StartDate,
                        b.EndDate,
                        b.ActualPrice,
                        CustomerName = b.BookingReservation.Customer.CustomerFullName,
                        RoomNumber = b.Room.RoomNumber
                    })
                    .ToList();

                dgdReportGrid.ItemsSource = reportData;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
        }
    }
}

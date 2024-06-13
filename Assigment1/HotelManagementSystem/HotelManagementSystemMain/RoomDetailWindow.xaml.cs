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
    /// Interaction logic for RoomDetailWindow.xaml
    /// </summary>
    public partial class RoomDetailWindow : Window
    {
        public RoomInformation SelectedRoom { get; set; } = null;  //mở lên ko có room nếu ko đc gán 
                                                                   // muốn có phải gán 
        private readonly RoomRepository _roomRepo;
        public RoomDetailWindow()
        {
           
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
             _roomRepo = new RoomRepository(context);
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cbxRoomType.ItemsSource = await _roomRepo.LoadRoomType();
            cbxRoomType.DisplayMemberPath = nameof(RoomType.RoomTypeName);
            cbxRoomType.SelectedValuePath = nameof(RoomType.RoomTypeId);
            
            //Check selectedRoom 
            if(SelectedRoom != null)
            {
                tbxRoomID.Text = SelectedRoom.RoomId.ToString();
                tbxRoomNumber.Text = SelectedRoom.RoomNumber.ToString();
                tbxRoomDetailDescription.Text = SelectedRoom.RoomDetailDescription?.ToString();
                tbxRoomMaxCapacity.Text = SelectedRoom.RoomMaxCapacity.ToString();
                cbxRoomType.SelectedValue = SelectedRoom.RoomTypeId;
                tbxPricePerDay.Text = SelectedRoom.RoomPricePerDay.ToString();
            }
             
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbxRoomMaxCapacity.Text, out int roomCapa))
            {
                MessageBox.Show("Please enter a valid number for room capacity.");
                return;
            }

            if (!decimal.TryParse(tbxPricePerDay.Text, out decimal pricePerDay))
            {
                MessageBox.Show("Please enter a valid number for price per day.");
                return;
            }

            
            RoomInformation roomInformation = new RoomInformation()
            {
                RoomNumber = tbxRoomNumber.Text,
                RoomDetailDescription = tbxRoomDetailDescription.Text,
                RoomMaxCapacity = roomCapa,
                RoomTypeId = (int)cbxRoomType.SelectedValue,
                RoomPricePerDay = pricePerDay,
                RoomStatus = 1 
                
            };
            await _roomRepo.AddOrUpdateRoomInfo(roomInformation);
            MessageBox.Show("Room information saved successfully.");
            ManageRoomWindow manageRoomWindow = new ManageRoomWindow();
            this.Close();
            manageRoomWindow.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageRoomWindow manageRoomWindow = new ManageRoomWindow();
            this.Close();
            manageRoomWindow.Show();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxRoomID.Text) || !int.TryParse(tbxRoomID.Text, out int roomInfoID))
            {
                MessageBox.Show("Please select a  room to delete.", "Invalid Room", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            await _roomRepo.DeleteRoomInfo(roomInfoID);
            MessageBox.Show("Room information delete successfully.");
            ManageRoomWindow manageRoomWindow = new ManageRoomWindow();
            this.Close();
            manageRoomWindow.Show();
        }

       
    }
}

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
    /// Interaction logic for ManageRoomWindow.xaml
    /// </summary>
    public partial class ManageRoomWindow : Window
    {
        private RoomRepository _roomRepo;
        public ManageRoomWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            _roomRepo = new RoomRepository(context);
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                var roomInfos = await _roomRepo.LoadAllRoomInfo();

                dgvRoomInfo.ItemsSource = roomInfos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoomDetailWindow roomDetail = new RoomDetailWindow();
            this.Close();
            roomDetail.ShowDialog();
        }

        private void dgvRoomInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Có thể chọn 1 dòng hay nhiều dòng
            //Láy dòng đầu tiên 
            if(dgvRoomInfo.SelectedCells.Count > 0)
            {
                RoomInformation selectedRoom = dgvRoomInfo.SelectedCells[0].Item as RoomInformation;
                RoomDetailWindow roomDetail = new RoomDetailWindow();
                roomDetail.SelectedRoom = selectedRoom;
                this.Close();
                roomDetail.ShowDialog();
                //Give to RoomDetailWindow
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = tbxSearchRoom.Text;
            if (search == null)
                LoadData();
            else
            {
                try
                {
                    var roomInfos = await _roomRepo.LoadAllRoomInfoBySearch(search);

                    dgvRoomInfo.ItemsSource = roomInfos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}

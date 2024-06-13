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
    /// Interaction logic for ManageCustomerWindow.xaml
    /// </summary>
    public partial class ManageCustomerWindow : Window
    {
        private UserRepository _userRepo;
        public ManageCustomerWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<FuminiHotelManagementContext>();
            var context = new FuminiHotelManagementContext(optionsBuilder.Options);
            _userRepo = new UserRepository(context);
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                var custInfos = await _userRepo.LoadAllCustInfo();

                dgvCustomerInfo.ItemsSource = custInfos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void dgvCustomerInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Có thể chọn 1 dòng hay nhiều dòng
            //Láy dòng đầu tiên 
            if (dgvCustomerInfo.SelectedCells.Count > 0)
            {
                Customer selectedCust = dgvCustomerInfo.SelectedCells[0].Item as Customer;
                CustomerDetailWindow custDetail = new CustomerDetailWindow();
                custDetail.SelectedCust = selectedCust;
                this.Close();
                custDetail.ShowDialog();
                //Give to RoomDetailWindow
            }
        }
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = tbxSearchCust.Text;
            if (search == null)
                LoadData();
            else
            {
                try
                {
                    var custInfos = await _userRepo.LoadAllCustInfoBySearch(search);

                    dgvCustomerInfo.ItemsSource = custInfos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailWindow custDetail = new CustomerDetailWindow();
            this.Close();
            custDetail.ShowDialog();

        }
    }
}

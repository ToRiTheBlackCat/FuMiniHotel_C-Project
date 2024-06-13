using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemMain.Models
{
    public class BookingDetailModel
    {
        public DateOnly? BookingDate { get; set; }
        public string RoomNumber { get; set; }
        public decimal? ActualPrice { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Price { get; set; }
        public byte? BookingStatus { get; set; }
    }
}

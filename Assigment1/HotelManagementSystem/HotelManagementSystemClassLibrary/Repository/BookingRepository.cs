using HotelManagementSystemMain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystemMain.Models;
namespace HotelManagementSystemClassLibrary.Repository
{
    public class BookingRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public async Task AddBookingReservation(BookingReservation bookingReservation)
        {
            await _context.BookingReservations.AddAsync(bookingReservation);
            await _context.SaveChangesAsync();
        }
        public List<BookingDetailModel> GetBookingsForCustomerAsync(Customer cust)
        {
            var custId = cust.CustomerId;
            var booknList = _context.BookingReservations.Where(br => br.CustomerId == custId).Include(br
                => br.BookingDetails).ThenInclude(bd => bd.Room).AsNoTracking().ToList();

            //var booknDetailList = booknList.Where(bd => bd.BookingReservationId == )

            var bookingDetailModels = new List<BookingDetailModel>();

            foreach (var bookingReservation in booknList)
            {
                foreach (var bookingDetail in bookingReservation.BookingDetails)
                {
                    var bookingDetailModel = new BookingDetailModel
                    {
                        BookingDate = bookingReservation.BookingDate,
                        ActualPrice = bookingDetail.ActualPrice,
                        BookingStatus = bookingReservation.BookingStatus,
                        RoomNumber = bookingDetail.Room.RoomNumber,
                        StartDate = bookingDetail.StartDate,
                        EndDate = bookingDetail.EndDate
                    };

                    bookingDetailModels.Add(bookingDetailModel);
                }
            }

            return bookingDetailModels;
        }







    }
}

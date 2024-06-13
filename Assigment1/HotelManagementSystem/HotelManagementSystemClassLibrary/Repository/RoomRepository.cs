using HotelManagementSystemMain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemClassLibrary.Repository
{
    public class RoomRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public RoomRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<List<RoomType>> LoadRoomType()
        {
            return await _context.RoomTypes.ToListAsync();
        }
        public async Task<List<RoomInformation>> LoadAllRoomInfo()
        {
            return await _context.RoomInformations.Where(r => r.RoomStatus == 1).Select(r => new RoomInformation
            {
                RoomId = r.RoomId,
                RoomNumber = r.RoomNumber,
                RoomDetailDescription = r.RoomDetailDescription,
                RoomMaxCapacity = r.RoomMaxCapacity,
                RoomTypeId = r.RoomTypeId,
                RoomStatus = r.RoomStatus,
                RoomPricePerDay = r.RoomPricePerDay
            }).ToListAsync();
        }
        public async Task<RoomInformation?> FindRoomInfoById(int? roomID)
        {
            return await _context.RoomInformations.FindAsync(roomID);
        }

        public async Task AddOrUpdateRoomInfo(RoomInformation roomInformation)
        {
            var roomInfo = await FindRoomInfoById(roomInformation.RoomId);
            if (roomInfo == null)
                _context.RoomInformations.Add(roomInformation);

            else
            {
                roomInfo.RoomNumber = roomInformation.RoomNumber;
                roomInfo.RoomDetailDescription = roomInformation.RoomDetailDescription;
                roomInfo.RoomMaxCapacity = roomInformation.RoomMaxCapacity;
                roomInfo.RoomTypeId = roomInformation.RoomTypeId;
                roomInfo.RoomPricePerDay = roomInformation.RoomPricePerDay;
                _context.RoomInformations.Update(roomInfo);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<RoomInformation>> LoadRoomInfo(int roomTypeId)
        {
            return await _context.RoomInformations.Where(r => r.RoomTypeId == roomTypeId).ToListAsync();
        }
        
        public async Task DeleteRoomInfo(int roomInfoID)
        {
            var roomInfo = await FindRoomInfoById(roomInfoID);
            roomInfo.RoomStatus = 0;
            _context.SaveChanges();
        }

        public async Task<List<RoomInformation>> LoadAllRoomInfoBySearch(string search) 
        { 
            return await _context.RoomInformations.Where(r => (r.RoomNumber.Contains(search) || r.RoomDetailDescription.Contains(search))
                                      && r.RoomStatus == 1).ToListAsync();
        }
    }
}

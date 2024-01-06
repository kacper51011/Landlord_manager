using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Domain.Interfaces
{
    public interface IRoomsStatisticsRepository
    {
        public Task<RoomsStatistics> GetRoomStatisticsById(string roomStatisticsId);
        public Task CreateOrUpdateRoomStatistics(RoomsStatistics room);
        public Task<RoomsStatistics> GetNotSendRoomsStatistics();
        public Task<RoomsStatistics> GetRoomAnyStatistics(int year, int? month, int? day, int? hour);
        public Task<RoomsStatistics> GetUnproccessedRoomStatistics();
    }
}

using Statistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Interfaces
{
    public interface IRoomsStatisticsRepository
    {
        public Task<RoomsStatistics> GetRoomsStatisticsById(string roomStatisticsId);
        public Task CreateOrUpdateRoomsStatistics(RoomsStatistics roomStatistics);
        public Task<List<RoomsStatistics>> GetNotProcessedStatistics();

        //gets
        public Task<RoomsStatistics> GetRoomsHourStatistics(int year, int month, int day, int hour);
        public Task<RoomsStatistics> GetRoomsDayStatistics(int year, int month, int day);
        public Task<RoomsStatistics> GetRoomsMonthStatistics(int year, int month);
        public Task<RoomsStatistics> GetRoomsYearStatistics(int year);
        public Task<RoomsStatistics> GetRoomsAnyStatistics(int year, int? month, int? day, int? hour);

        public Task<RoomsStatistics> GetNotSentRoomStatistics();

    }
}

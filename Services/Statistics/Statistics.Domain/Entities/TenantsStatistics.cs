using MongoDB.Bson.Serialization.Serializers;
using Statistics.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class TenantsStatistics: AggregateRoot
    {
        private TenantsStatistics()
        {
            TenantsStatisticsId = Guid.NewGuid().ToString();
            TenantsCreated = 0;
            TenantsUpdated = 0;
            HighestRent = 0;
            MostTenantsInRoom = 0;
            IsFullyProcessed = false;
        }
        public string TenantsStatisticsId { get; private set; }   
        public Year Year { get; private set; }
        public Month? Month { get; private set; }
        public Day? Day { get; private set; }
        public Hour? Hour { get; private set; }
        public StatisticsStart StatisticsStart { get; private set; }
        public StatisticsEnd StatisticsEnd { get; private set; }
        public int TenantsCreated { get; private set; }
        public int TenantsUpdated { get; private set; }
        public int HighestRent { get; private set; }
        public int MostTenantsInRoom { get; private set; }
        public bool IsSent { get; private set; }
        public bool IsFullyProcessed { get; private set; }
        public string Scope { get; private set; }

        public static TenantsStatistics CreateAsHourStatisticsInformations(int year, int month, int day, int hour, bool isSent)
        {
            return new TenantsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = new Hour(hour),
                Scope = "Hour",
                StatisticsStart =new StatisticsStart( new DateTime(year, month, day, hour, 1, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, day, hour + 1, 1, 1)),
                IsSent = isSent
            };

        }
        public static TenantsStatistics CreateAsDayStatisticsInformations(int year, int month, int day, bool isSent )
        {
            return new TenantsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = null,
                Scope = "Day",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, day)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, day + 1)),
                IsSent = isSent
            };
        }

        public static TenantsStatistics CreateAsMonthStatisticsInformations(int year, int month, bool isSent)
        {
            return new TenantsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = null,
                Hour = null,
                Scope = "Month",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month + 1, 1)),
                IsSent = isSent
            };
        }

        public static TenantsStatistics CreateAsYearStatisticsInformations(int year, bool isSent)
        {
            return new TenantsStatistics()
            {
                Year = new Year(year),
                Month = null,
                Day = null,
                Hour = null,
                Scope = "Year",
                StatisticsStart = new StatisticsStart(new DateTime(year, 1, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year + 1, 1, 1)),
                IsSent = isSent
            };

        }

        public void SetStatistics(int roomsCreated, int roomsUpdated, int highestRent, int mostTenantsInRoom)
        {
            TenantsCreated = roomsCreated;
            TenantsUpdated = roomsUpdated;
            HighestRent = highestRent;
            MostTenantsInRoom = mostTenantsInRoom;
        }

        public void SetIsSent(bool isSent)
        {
            IsSent = isSent;
        }
    }


}

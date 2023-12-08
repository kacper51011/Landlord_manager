using Statistics.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class ApartmentsStatistics : AggregateRoot
    {
        private ApartmentsStatistics()
        {
            ApartmentsStatisticsId = Guid.NewGuid().ToString();
            ApartmentsCreated = 0;
            ApartmentsUpdated = 0;
            MostApartmentsOwnedByUser = 0;
            IsFullyProcessed = false;
        }

        public string ApartmentsStatisticsId { get; private set; }
        public Year Year { get; private set; }
        public Month? Month { get; private set; }
        public Day? Day { get; private set; }
        public Hour? Hour { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public int ApartmentsUpdated { get; private set; }
        public int MostApartmentsOwnedByUser { get; private set; }
        public bool IsFullyProcessed { get; private set; }
        public string Scope { get; private set; }

        public static ApartmentsStatistics CreateAsHourStatisticsInformations(int year, int month, int day, int hour)
        {
            return new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = new Hour(hour),
                Scope = "Hour",
                StatisticsStart = new DateTime(year, month, day, hour, 1, 1),
                StatisticsEnd = new DateTime(year, month, day, hour + 1, 1, 1)
            };

        }
        public static ApartmentsStatistics CreateAsDayStatisticsInformations(int year, int month, int day)
        {
            return new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = null,
                Scope = "Day",
                StatisticsStart = new DateTime(year, month, day),
                StatisticsEnd = new DateTime(year, month, day + 1)
            };
        }

        public static ApartmentsStatistics CreateAsMonthStatisticsInformations(int year, int month)
        {
            return new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = null,
                Hour = null,
                Scope = "Month",
                StatisticsStart = new DateTime(year, month, 1),
                StatisticsEnd = new DateTime(year, month + 1, 1)
            };
        }

        public static ApartmentsStatistics CreateAsYearStatisticsInformations(int year)
        {
            return new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = null,
                Day = null,
                Hour = null,
                Scope = "Year",
                StatisticsStart = new DateTime(year, 1, 1),
                StatisticsEnd = new DateTime(year + 1, 1,1)
            };
        
        }

        public void SetStatistics(int apartmentsCreated, int apartmentsUpdated, int mostOwned)
        {
            ApartmentsCreated = apartmentsCreated;
            ApartmentsUpdated = apartmentsUpdated;
            MostApartmentsOwnedByUser = mostOwned;
            IsFullyProcessed = true;
        }
    }
}

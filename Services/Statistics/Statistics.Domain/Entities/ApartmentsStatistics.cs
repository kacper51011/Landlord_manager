using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class ApartmentsStatistics : AggregateRoot
    {
        private ApartmentsStatistics(DateTime start, DateTime end, string scope)
        {
            ApartmentsStatisticsId = Guid.NewGuid().ToString();
            StatisticsStart = start;
            StatisticsEnd = end;
            ApartmentsCreated = 0;
            ApartmentsUpdated = 0;
            MostApartmentsOwnedByUser = 0;
            Scope = scope;
            IsFullyProcessed = false;
        }

        public string ApartmentsStatisticsId { get; private set; }
        public int Year { get; private set; }
        public int? Month { get; private set; }
        public int? Day { get; private set; }
        public int? Hour { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public int ApartmentsUpdated { get; private set; }
        public int MostApartmentsOwnedByUser { get; private set; }
        public bool IsFullyProcessed { get; private set; }
        public string Scope { get; private set; }


        public static ApartmentsStatistics CreateEmpty(DateTime statisticsStart, DateTime statisticsEnd, string scope)
        {
            return new ApartmentsStatistics(statisticsStart, statisticsEnd, scope);
        }

        public void Update(ApartmentsStatistics statistics)
        {
            ApartmentsCreated = statistics.ApartmentsCreated;
            ApartmentsUpdated = statistics.ApartmentsUpdated;
            MostApartmentsOwnedByUser = statistics.MostApartmentsOwnedByUser;
        }

        public void SetHourStatisticsInformations(DateTime statisticsStart)
        {
            Year = statisticsStart.Year;
            Month = statisticsStart.Month;
            Day = statisticsStart.Day;
            Hour = statisticsStart.Hour;
            Scope = "Hour";

        }
        public void SetDayStatisticsInformations(DateTime statisticsStart)
        {
            Year = statisticsStart.Year;
            Month = statisticsStart.Month;
            Day = statisticsStart.Day;
            Hour = null;
            Scope = "Day";
        }

        public void SetMonthStatisticsInformations(DateTime statisticsStart)
        {
            Year = statisticsStart.Year;
            Month = statisticsStart.Month;
            Day = null;
            Hour = null;
            Scope = "Month";
        }

        public void SetYearStatisticsInformations(DateTime statisticsStart)
        {
            Year = statisticsStart.Year;
            Month = null;
            Day = null;
            Hour = null;
            Scope = "Year";
        }


    }
}

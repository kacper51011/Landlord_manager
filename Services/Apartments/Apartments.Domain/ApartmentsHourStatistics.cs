using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain
{
    public class ApartmentsHourStatistics: AggregateRoot
    {
        private ApartmentsHourStatistics(string idFromStatistics, DateTime start, DateTime end)
        {
            ApartmentsStatisticsId = idFromStatistics;
            StatisticsStart = start;
            StatisticsEnd = end;
            ApartmentsCreated = 0;
            IsApartmentsCreatedProcessed = false;
            ApartmentsUpdated = 0;
            IsApartmentsUpdatedProcessed = false;
            MostApartmentsOwnedByUser = 0;
            IsMostApartmentsOwnedByUserProcessed = false;
        }

        public string ApartmentsStatisticsId { get; private set; }
        public DateTime StatisticsStart { get; private set; }
        public DateTime StatisticsEnd { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public bool IsApartmentsCreatedProcessed { get; private set; }
        public int ApartmentsUpdated { get; private set; }
        public bool IsApartmentsUpdatedProcessed { get; private set; }
        public int MostApartmentsOwnedByUser { get; private set; }
        public bool IsMostApartmentsOwnedByUserProcessed { get; private set; }
        public bool IsSavedByStatisticsService { get; private set; }


        public ApartmentsHourStatistics Create(string idFromStatistics, DateTime statisticsStart, DateTime statisticsEnd)
        {
            return new ApartmentsHourStatistics(idFromStatistics, statisticsStart, statisticsEnd);
        }

        public void ProcessApartmentsCreatedCount(int apartmentsCreated)
        {
            ApartmentsCreated = apartmentsCreated;
            IsApartmentsCreatedProcessed = true;
        }

        public void ProcessApartmentsUpdatedCount(int apartmentsUpdated)
        {
            ApartmentsUpdated = apartmentsUpdated;
            IsApartmentsUpdatedProcessed = true;
        }
        public void ProcessMostApartmentsOwnedByUser(int apartmentsOwnedByUser)
        {
            MostApartmentsOwnedByUser = apartmentsOwnedByUser;
            IsMostApartmentsOwnedByUserProcessed = true;
        }
    }
}

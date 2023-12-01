using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Domain.Entities
{
    public class ApartmentsStatistics: AggregateRoot
    {
        private ApartmentsStatistics(DateTime start, DateTime end)
        {
            ApartmentsStatisticsId = Guid.NewGuid().ToString();
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

    public ApartmentsStatistics Create(DateTime statisticsStart, DateTime statisticsEnd)
    {
        return new ApartmentsStatistics(statisticsStart, statisticsEnd);
    }
    }


}

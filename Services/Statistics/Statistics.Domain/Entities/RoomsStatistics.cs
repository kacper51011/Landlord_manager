using Statistics.Domain.ValueObjects;

namespace Statistics.Domain.Entities
{
    public class RoomsStatistics : AggregateRoot
    {
        private RoomsStatistics()
        {
            RoomsStatisticsId = Guid.NewGuid().ToString();
            RoomsCreated = 0;
            RoomsUpdated = 0;
            BiggestCreatedRoomSize = 0;
            MostRoomsInApartment = 0;
            IsFullyProcessed = false;
        }
        public string RoomsStatisticsId { get; private set; }
        public Year Year { get; private set; }
        public Month? Month { get; private set; }
        public Day? Day { get; private set; }
        public Hour? Hour { get; private set; }
        public StatisticsStart StatisticsStart { get; private set; }
        public StatisticsEnd StatisticsEnd { get; private set; }
        public int RoomsCreated { get; private set; }
        public int RoomsUpdated { get; private set; }
        public int BiggestCreatedRoomSize { get; private set; }
        public int MostRoomsInApartment { get; private set; }
        public bool IsSent { get; private set; }
        public bool IsFullyProcessed { get; private set; }
        public string Scope { get; private set; }


        public static RoomsStatistics CreateAsHourStatisticsInformations(int year, int month, int day, int hour, bool isSent)
        {
            return new RoomsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = new Hour(hour),
                Scope = "Hour",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, day, hour, 1, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, day, hour + 1, 1, 1)),
                IsSent = isSent
            };

        }
        public static RoomsStatistics CreateAsDayStatisticsInformations(int year, int month, int day, bool isSent)
        {
            return new RoomsStatistics()
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

        public static RoomsStatistics CreateAsMonthStatisticsInformations(int year, int month, bool isSent)
        {
            return new RoomsStatistics()
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

        public static RoomsStatistics CreateAsYearStatisticsInformations(int year, bool isSent)
        {
            return new RoomsStatistics()
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

        public void SetStatistics(int roomsCreated, int roomsUpdated, int biggestCreatedRoomSize, int mostRoomsInApartment)
        {
            RoomsCreated = roomsCreated;
            RoomsUpdated = roomsUpdated;
            BiggestCreatedRoomSize = biggestCreatedRoomSize;
            MostRoomsInApartment = mostRoomsInApartment;
        }

        public void SetIsSent(bool isSent)
        {
            IsSent = isSent;
        }
    }

}

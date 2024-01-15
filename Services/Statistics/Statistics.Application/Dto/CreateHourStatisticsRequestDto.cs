namespace Statistics.Application.Dto
{
    public class CreateHourStatisticsRequestDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
    }
}

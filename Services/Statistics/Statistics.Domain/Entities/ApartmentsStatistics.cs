﻿using Statistics.Domain.ValueObjects;
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
        public StatisticsStart StatisticsStart { get; private set; }
        public StatisticsEnd StatisticsEnd { get; private set; }
        public int ApartmentsCreated { get; private set; }
        public int ApartmentsUpdated { get; private set; }
        public int MostApartmentsOwnedByUser { get; private set; }
        public bool IsFullyProcessed { get; private set; }
        public bool IsSent { get; private set; }
        public string Scope { get; private set; }

        public static ApartmentsStatistics CreateAsHourStatisticsInformations(int year, int month, int day, int hour, bool isSent)
        {
            var apartmentStatistic = new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = new Hour(hour),
                Scope = "Hour",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, day, hour, 1, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, day, hour, 1, 1).AddHours(1)),
                IsSent = isSent
            };
            apartmentStatistic.SetCreationDate();
            apartmentStatistic.SetLastModifiedDate();
            apartmentStatistic.IncrementVersion();
            return apartmentStatistic;

        }
        public static ApartmentsStatistics CreateAsDayStatisticsInformations(int year, int month, int day, bool isSent)
        {
            var apartmentStatistic = new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = new Day(day),
                Hour = null,
                Scope = "Day",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, day)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, day).AddDays(1)),
                IsSent = isSent
            };
            apartmentStatistic.SetCreationDate();
            apartmentStatistic.SetLastModifiedDate();
            apartmentStatistic.IncrementVersion();
            return apartmentStatistic;
        }

        public static ApartmentsStatistics CreateAsMonthStatisticsInformations(int year, int month, bool isSent )
        {
            var apartmentStatistic = new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = new Month(month),
                Day = null,
                Hour = null,
                Scope = "Month",
                StatisticsStart = new StatisticsStart(new DateTime(year, month, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, month, 1).AddMonths(1)),
                IsSent = isSent
            };
            apartmentStatistic.SetCreationDate();
            apartmentStatistic.SetLastModifiedDate();
            apartmentStatistic.IncrementVersion();
            return apartmentStatistic;
        }

        public static ApartmentsStatistics CreateAsYearStatisticsInformations(int year, bool isSent)
        {
            var apartmentStatistic = new ApartmentsStatistics()
            {
                Year = new Year(year),
                Month = null,
                Day = null,
                Hour = null,
                Scope = "Year",
                StatisticsStart = new StatisticsStart(new DateTime(year, 1, 1)),
                StatisticsEnd = new StatisticsEnd(new DateTime(year, 1,1).AddYears(1)),
                IsSent = isSent

            };
            apartmentStatistic.SetCreationDate();
            apartmentStatistic.SetLastModifiedDate();
            apartmentStatistic.IncrementVersion();
            return apartmentStatistic;

        }

        public void SetStatistics(int apartmentsCreated, int apartmentsUpdated, int mostOwned)
        {
            ApartmentsCreated = apartmentsCreated;
            ApartmentsUpdated = apartmentsUpdated;
            MostApartmentsOwnedByUser = mostOwned;
            IsFullyProcessed = true;
        }

        public void SetIsSent(bool isSent)
        {
            IsSent = isSent;
        }
    }
}

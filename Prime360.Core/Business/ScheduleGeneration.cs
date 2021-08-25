using System;
using System.Collections.Generic;

namespace Prime360.Core.Business
{
    public static class ScheduleGeneration
    {
        public static List<DateTime> GenerateWeeklySchedule(DateTime startDate, DateTime lastPossibleDate)
        {
            if (lastPossibleDate < startDate)
            {
                throw new ArgumentException("The last possible date cannot be less than the start date");
            }

            return GenerateSchedule(startDate, lastPossibleDate, 7);
        }

        public static List<DateTime> GenerateBiWeeklySchedule(DateTime startDate, DateTime lastPossibleDate)
        {
            if (lastPossibleDate < startDate)
            {
                throw new ArgumentException("The last possible date cannot be less than the start date");
            }
            return GenerateSchedule(startDate, lastPossibleDate, 14);
        }

        /// <summary>
        /// Method will generate a monthly schedule such that each payment is on the same day every month.
        /// 
        /// If a payment falls on a day that doesn't exist in that month(the 31st on Feb), it will select the next closest date
        /// </summary>
        /// <returns></returns>

        public static List<DateTime> GenerateMonthlySchedule(DateTime startDate, DateTime lastPossibleDate)
        {
            var schedule = new List<DateTime>();
            DateTime currentDate = startDate;
            int monthlyDay = startDate.Day;
            //july -> 31
            //aug -> 31
            //sept -> 30
            //oct -> 31
            
            while (currentDate <= lastPossibleDate)
            {
                schedule.Add(currentDate);
                currentDate = currentDate.AddMonths(1);
                if(monthlyDay != currentDate.Day && DateTime.DaysInMonth(currentDate.Year, currentDate.Month) == monthlyDay)
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, monthlyDay);
                }
            }

            return schedule;
        }

        private static List<DateTime> GenerateSchedule(DateTime startDate, DateTime lastPossibleDate, int terms)
        {
            var schedule = new List<DateTime>();
            DateTime currentDate = startDate;
            while (currentDate <= lastPossibleDate)
            {
                schedule.Add(currentDate);
                currentDate = currentDate.AddDays(terms);
            }
            return schedule;
        }
    }
}

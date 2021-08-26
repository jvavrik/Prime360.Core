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
            if (lastPossibleDate < startDate)
            {
                throw new ArgumentException("The last possible date cannot be less than the start date");
            }

            var schedule = new List<DateTime>();
            DateTime currentDate = startDate;
            int monthlyDay = startDate.Day;

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

        public static List<DateTime> GenerateBiMonthlySchedule(DateTime startDate, DateTime secondDate, DateTime lastPossibleDate)
        {
            if (lastPossibleDate < startDate || lastPossibleDate < secondDate)
            {
                throw new ArgumentException("The last possible date cannot be less than the start date or second date");
            }
            if (secondDate < startDate)
            {
                throw new ArgumentException("The second date cannot be less than the start date");
            }

            var schedule = new List<DateTime>();
            DateTime currentDate = startDate;
            int firstDay = startDate.Day;
            int secondDay = secondDate.Day;
            
            while (currentDate <= lastPossibleDate)
            {

                if (firstDay < secondDay)
                {
                    schedule.Add(currentDate);
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, secondDay);
                    schedule.Add(currentDate);
                    currentDate = new DateTime(currentDate.Year, currentDate.Month + 1, firstDay);
                }
                else
                {
                    schedule.Add(currentDate);
                    currentDate = new DateTime(currentDate.Year, currentDate.Month + 1, secondDay);
                    if (currentDate <= lastPossibleDate)
                    {
                        schedule.Add(currentDate);
                        currentDate = new DateTime(currentDate.Year, currentDate.Month, firstDay);
                    }
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

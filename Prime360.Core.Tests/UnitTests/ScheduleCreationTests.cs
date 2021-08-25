using System;
using System.Collections.Generic;
using Xunit;
using Prime360.Core.Business;

namespace Prime360.Core.Tests.UnitTests
{
    public class ScheduleCreationTests
    {
        [Fact]
        public void WeeklySchedule_GivenAStartDate_AWeeklyScheduleIsCreatedNotPassingLastPossibleDate()
        {
            var schedule = ScheduleGeneration.GenerateWeeklySchedule(new DateTime(2020, 3, 16), new DateTime(2020, 8, 16));

            Assert.Equal(22, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 10), schedule[schedule.Count-1]);
        }
        [Fact]
        public void WeeklySchedule_GivenAStartDate_AnErrorIsThrownIfLastPossibleDateIsBeforeStartDate()
        {
            var scheduleException = Assert.Throws<ArgumentException>(() => ScheduleGeneration.GenerateWeeklySchedule(new DateTime(2020, 3, 16), new DateTime(2019, 8, 16)));

            Assert.Equal("The last possible date cannot be less than the start date", scheduleException.Message);
        }
        [Fact]
        public void BiWeeklySchedule_GivenAStartDate_ABiWeeklyScheduleIsCreatedNotPassingLastPossibleDate()
        {
            var schedule = ScheduleGeneration.GenerateBiWeeklySchedule(new DateTime(2020, 3, 16), new DateTime(2020, 8, 16));

            Assert.Equal(11, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 3), schedule[schedule.Count - 1]);
        }
        [Fact]
        public void BiWeeklySchedule_GivenAStartDate_AnErrorIsThrownIfLastPossibleDateIsBeforeStartDate()
        {
            var scheduleException = Assert.Throws<ArgumentException>(() => ScheduleGeneration.GenerateBiWeeklySchedule(new DateTime(2020, 3, 16), new DateTime(2019, 8, 16)));

            Assert.Equal("The last possible date cannot be less than the start date", scheduleException.Message);
        }

        [Fact]
        public void MonthlySchedule_GivenAStartDate_AMonthlyScheduleIsCreatedNotPassingLastPossibleDate()
        {
            var schedule = ScheduleGeneration.GenerateMonthlySchedule(new DateTime(2020, 3, 16), new DateTime(2020, 9, 15));

            Assert.Equal(6, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 16), schedule[schedule.Count - 1]);
        }

        [Fact]
        public void MonthlySchedule_GivenAStartDate_AMonthlyScheduleIsCreatedNotPassingLastPossibleDate_UsingLastDayOfMonth()
        {
            var schedule = ScheduleGeneration.GenerateMonthlySchedule(new DateTime(2020, 3, 31), new DateTime(2020, 9, 15));

            Assert.Equal(6, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 31), schedule[schedule.Count - 1]);
        }

        [Fact]
        public void MonthlySchedule_GivenAStartDate_AMonthlyScheduleIsCreatedNotPassingLastPossibleDate_UsingLastDayOfMonth_AndFeb()
        {
            var schedule = ScheduleGeneration.GenerateMonthlySchedule(new DateTime(2020, 1, 31), new DateTime(2020, 9, 15));

            Assert.Equal(8, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 31), schedule[schedule.Count - 1]);
        }

        [Fact]
        public void MonthlySchedule_GivenAStartDate_AMonthlyScheduleIsCreatedNotPassingLastPossibleDate_UsingSecondToLastDayOfMonth_AndFeb()
        {
            var schedule = ScheduleGeneration.GenerateMonthlySchedule(new DateTime(2020, 1, 30), new DateTime(2020, 9, 15));

            Assert.Equal(8, schedule.Count);
            Assert.Equal(new DateTime(2020, 8, 30), schedule[schedule.Count - 1]);
        }

    }
}

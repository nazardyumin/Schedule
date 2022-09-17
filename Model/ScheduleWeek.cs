using System;

namespace Schedule.Model
{
    public class ScheduleWeek
    {
        public ScheduleDay[]? Week { get; set; }
        public ScheduleWeek ()
        {
            Week = new ScheduleDay[7];
            for (int i= 0; i<7;i++)
            {
                Week[i] = new ScheduleDay() { Date= DateTime.Now };
            }
        }
    }
}

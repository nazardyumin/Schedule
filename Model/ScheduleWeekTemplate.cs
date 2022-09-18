using System;

namespace Schedule.Model
{
    public class ScheduleWeekTemplate
    {
        public Day[]? Week { get; set; }
        public ScheduleWeekTemplate ()
        {
            Week = new Day[7];
            for (int i= 0; i<7;i++)
            {
                Week[i] = new Day() { Date= DateTime.Now };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Schedule.Model
{
    public class WeekTemplate : Notifier
    {
        private List<Day>? Days; 

        private Day? _monday;
        public Day Monday 
        { 
            get => _monday!; 
            set => SetField(ref _monday, value); 
        }

        private Day? _tuesday;
        public Day Tuesday
        {
            get => _tuesday!;
            set => SetField(ref _tuesday, value);
        }

        private Day? _wednesday;
        public Day Wednesday
        {
            get => _wednesday!;
            set => SetField(ref _wednesday, value);
        }

        private Day? _thursday;
        public Day Thursday
        {
            get => _thursday!;
            set => SetField(ref _thursday, value);
        }

        private Day? _friday;
        public Day Friday
        {
            get => _friday!;
            set => SetField(ref _friday, value);
        }

        private Day? _saturday;
        public Day Saturday
        {
            get => _saturday!;
            set => SetField(ref _saturday, value);
        }

        private Day? _sunday;
        public Day Sunday
        {
            get => _sunday!;
            set => SetField(ref _sunday, value);
        }

        public WeekTemplate ()
        {
            SetDays();
            FocuseOnCurrentWeek();
        }

        private void SetDays()
        {
            Days = new();
            for (int i=0;i<365;i++)
            {
                var timeSpan = new TimeSpan(i,0,0,0);
                var date = new DateTime(2022, 1, 1);
                Days.Add(new Day(date + timeSpan));
            }
        }
        private int FocuseOnCurrentWeek()
        {
            DateTime currentDate = DateTime.Now;
            int index;
            int currentDayOfTheWeek = 0;
            foreach (var item in from item in Days!
                                 where item.IsCurrentDay(currentDate.Year, currentDate.Month, currentDate.Day)
                                 select item)
            {
                index = Days!.IndexOf(item);
                currentDayOfTheWeek = Days[index].GetDayIndex();
                var lastIndex = Days.Count - 1;
                switch (currentDayOfTheWeek)
                { 
                    case 0:
                        Monday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Tuesday = Days[index+1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Wednesday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Thursday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Friday = Days[index + 4];
                        }
                        if (index + 5 <= lastIndex)
                        {
                            Saturday = Days[index + 5];
                        }
                        if (index + 6 <= lastIndex)
                        {
                            Sunday = Days[index + 6];
                        }
                        break;
                    case 1:
                        if (index - 1 >= 0)
                        {
                            Monday = Days[index-1];
                        }
                        Tuesday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Wednesday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Thursday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Friday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Saturday = Days[index + 4];
                        }
                        if (index + 5 <= lastIndex)
                        {
                            Sunday = Days[index + 5];
                        }
                        break;
                    case 2:
                        if (index - 2 >= 0)
                        {
                            Monday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Tuesday = Days[index - 1];
                        }
                        Wednesday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Thursday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Friday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Saturday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Sunday = Days[index + 4];
                        }
                        break;
                    case 3:
                        if (index - 3 >= 0)
                        {
                            Monday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Tuesday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Wednesday = Days[index - 1];
                        }
                        Thursday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Friday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Saturday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Sunday = Days[index + 3];
                        }
                        break;
                    case 4:
                        if (index - 4 >= 0)
                        {
                            Monday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Tuesday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Wednesday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Thursday = Days[index - 1];
                        }
                        Friday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Saturday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Sunday = Days[index + 2];
                        }
                        break;
                    case 5:
                        if (index - 5 >= 0)
                        {
                            Monday = Days[index - 5];
                        }
                        if (index - 4 >= 0)
                        {
                            Tuesday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Wednesday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Thursday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Friday = Days[index - 1];
                        }
                        Saturday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Sunday = Days[index + 1];
                        }
                        break;
                    case 6:
                        if (index - 6 >= 0)
                        {
                            Monday = Days[index - 6];
                        }
                        if (index - 5 >= 0)
                        {
                            Tuesday = Days[index - 5];
                        }
                        if (index - 4 >= 0)
                        {
                            Wednesday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Thursday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Friday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Saturday = Days[index - 1];
                        }
                        Sunday = Days[index];
                        break;
                }
            }
            return currentDayOfTheWeek;
        }
    }
}

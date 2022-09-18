using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Schedule.Model
{
    public class WeekTemplate : Notifier
    {
        private List<Day>? Days;
        private int _currentDayIndex;

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

        private string? _header;
        public string Header
        {
            get => _header!;
            set => SetField(ref _header, value);
        }
        public List<int>? ComboBoxYears{ get; set; }
        public List<string>? ComboBoxMonths { get; set; }
        public List<int>? ComboBoxDates{get;set;}
        public List<string>? ComboBoxTime { get; set; }

        public WeekTemplate ()
        {
            SetDays();
            SetComboBoxYears();
            SetComboBoxMonths();
            SetComboBoxDates();
            SetComboBoxTime();
            FocuseOnCurrentWeek();
        }

        private void SetDays()
        {
            Days = new();
            for (int i=0;i<1000;i++)
            {
                var timeSpan = new TimeSpan(i,0,0,0);
                var date = new DateTime(2022, 1, 1);
                Days.Add(new Day(date + timeSpan));
            }
        }
        private void FocuseOnCurrentWeek()
        {
            DateTime currentDate = DateTime.Now;
            int index=0;
            int currentDayOfTheWeek;
            foreach (var item in Days!)
            {
                if (item.IsCurrentDay(currentDate.Year, currentDate.Month, currentDate.Day))
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
                                Tuesday = Days[index + 1];
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
                            Header = SetHeader();
                            break;
                        case 1:
                            if (index - 1 >= 0)
                            {
                                Monday = Days[index - 1];
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
                            Header = SetHeader();
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
                            Header = SetHeader();
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
                            Header = SetHeader();
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
                            Header = SetHeader();
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
                            Header = SetHeader();
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
                            Header = SetHeader();
                            break;
                    }
                    break;
                }
            }                        
            _currentDayIndex=index;
        }
        private string SetHeader()
        {
            return $"{Monday.GetDayInfo()} - {Sunday.GetDayInfo()}";
        }
        public int GetCurrentDayIndex()
        {
            return _currentDayIndex;
        }
        public void AddCardToGrid(Card card, ref Grid grid)
        {
            grid.Children.Add(card);
        }
        public void NextWeek()
        {
            Monday = Days![Days.IndexOf(Monday) + 7];
            Tuesday = Days![Days.IndexOf(Tuesday) + 7];
            Wednesday = Days![Days.IndexOf(Wednesday) + 7];
            Thursday = Days![Days.IndexOf(Thursday) + 7];
            Friday = Days![Days.IndexOf(Friday) + 7];
            Saturday = Days![Days.IndexOf(Saturday) + 7];
            Sunday = Days![Days.IndexOf(Sunday) + 7];
            Header=SetHeader();
        }
        public void PreviousWeek()
        {
            Monday = Days![Days.IndexOf(Monday) - 7];
            Tuesday = Days![Days.IndexOf(Tuesday) - 7];
            Wednesday = Days![Days.IndexOf(Wednesday) - 7];
            Thursday = Days![Days.IndexOf(Thursday) - 7];
            Friday = Days![Days.IndexOf(Friday) - 7];
            Saturday = Days![Days.IndexOf(Saturday) - 7];
            Sunday = Days![Days.IndexOf(Sunday) - 7];
            Header = SetHeader();
        }
        private void SetComboBoxYears()
        {
            ComboBoxYears = new();
            ComboBoxYears.Add(2022);
            ComboBoxYears.Add(2023);
        }
        private void SetComboBoxMonths()
        {
            ComboBoxMonths = new();
            ComboBoxMonths.Add("January");
            ComboBoxMonths.Add("February");
            ComboBoxMonths.Add("March");
            ComboBoxMonths.Add("April");
            ComboBoxMonths.Add("May");
            ComboBoxMonths.Add("June");
            ComboBoxMonths.Add("July");
            ComboBoxMonths.Add("August");
            ComboBoxMonths.Add("September");
            ComboBoxMonths.Add("October");
            ComboBoxMonths.Add("November");
            ComboBoxMonths.Add("December");
        }
        private void SetComboBoxDates()
        {
            ComboBoxDates = new();
            for (int i = 0; i < 31; i++)
            {
                ComboBoxDates.Add(i + 1);
            }
        }
        private void SetComboBoxTime()
        {
            ComboBoxTime = new();
            ComboBoxTime.Add("08:00");
            ComboBoxTime.Add("08:10");
            ComboBoxTime.Add("08:20");
            ComboBoxTime.Add("08:30");
            ComboBoxTime.Add("08:40");
            ComboBoxTime.Add("08:50");
            ComboBoxTime.Add("09:00");
            ComboBoxTime.Add("09:10");
            ComboBoxTime.Add("09:20");
            ComboBoxTime.Add("09:30");
            ComboBoxTime.Add("09:40");
            ComboBoxTime.Add("09:50");
            ComboBoxTime.Add("10:00");
            ComboBoxTime.Add("10:10");
            ComboBoxTime.Add("10:20");
            ComboBoxTime.Add("10:30");
            ComboBoxTime.Add("10:40");
            ComboBoxTime.Add("10:50");
            ComboBoxTime.Add("11:00");
        }
        public (int year, int month, int day) GetTodayIndexes()
        {
            DateTime today = DateTime.Now;
            return (today.Year - 2022, today.Month - 1, today.Day - 1);
        }
    }
}

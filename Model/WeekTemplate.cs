using System;
using System.Collections.Generic;
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
        public List<int>? ComboBoxYears { get; set; }
        public List<string>? ComboBoxMonths { get; set; }
        public List<int>? ComboBoxDates { get; set; }
        public List<string>? ComboBoxTime { get; set; }
        public List<string>? ComboBoxDays { get; set; }

        public WeekTemplate()
        {
            SetDays();
            FocuseOnCurrentWeek();
        }

        private void SetDays()
        {
            Days = new();
            for (int i = 0; i < 1000; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                var date = new DateTime(2022, 1, 1);
                Days.Add(new Day(date + timeSpan));
            }
        }
        public void FocuseOnCurrentWeek()
        {
            DateTime currentDate = DateTime.Now;
            int index;
            int currentDayOfTheWeek = 0;
            foreach (var item in Days!)
            {
                if (item.IsThisDay(currentDate.Year, currentDate.Month, currentDate.Day))
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
            _currentDayIndex = currentDayOfTheWeek;
        }
        private string SetHeader()
        {
            return $"{Monday.GetDayInfo()} - {Sunday.GetDayInfo()}";
        }
        public int GetCurrentDayIndex()
        {
            return _currentDayIndex;
        }

        public (bool isCurrentWeek, bool isFuture, int index) NextWeek()
        {
            DateTime now = DateTime.Now;
            Monday = Days![Days.IndexOf(Monday) + 7];
            Tuesday = Days![Days.IndexOf(Tuesday) + 7];
            Wednesday = Days![Days.IndexOf(Wednesday) + 7];
            Thursday = Days![Days.IndexOf(Thursday) + 7];
            Friday = Days![Days.IndexOf(Friday) + 7];
            Saturday = Days![Days.IndexOf(Saturday) + 7];
            Sunday = Days![Days.IndexOf(Sunday) + 7];
            Header = SetHeader();
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            if (Monday.IsFuture()) return (false, true, 0);
            if (Tuesday.IsFuture()) return (false, true, 1);
            if (Wednesday.IsFuture()) return (false, true, 2);
            if (Thursday.IsFuture()) return (false, true, 3);
            if (Friday.IsFuture()) return (false, true, 4);
            if (Saturday.IsFuture()) return (false, true, 5);
            if (Sunday.IsFuture()) return (false, true, 6);
            return (false, false, -1);
        }
        public (bool isCurrentWeek, bool isFuture, int index) PreviousWeek()
        {
            DateTime now = DateTime.Now;
            Monday = Days![Days.IndexOf(Monday) - 7];
            Tuesday = Days![Days.IndexOf(Tuesday) - 7];
            Wednesday = Days![Days.IndexOf(Wednesday) - 7];
            Thursday = Days![Days.IndexOf(Thursday) - 7];
            Friday = Days![Days.IndexOf(Friday) - 7];
            Saturday = Days![Days.IndexOf(Saturday) - 7];
            Sunday = Days![Days.IndexOf(Sunday) - 7];
            Header = SetHeader();
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            if (Monday.IsFuture()) return (false, true, 0);
            if (Tuesday.IsFuture()) return (false, true, 1);
            if (Wednesday.IsFuture()) return (false, true, 2);
            if (Thursday.IsFuture()) return (false, true, 3);
            if (Friday.IsFuture()) return (false, true, 4);
            if (Saturday.IsFuture()) return (false, true, 5);
            if (Sunday.IsFuture()) return (false, true, 6);
            return (false, false, -1);
        }

        public List<Day> GetDays()
        {
            return Days!;
        }
        public Lesson CreateLesson(string subject, string teacher, string auditorium, int startTimeIndex, int endTimeIndex, string time)
        {
            var lesson = new Lesson(subject, teacher, auditorium, time);
            lesson.SetPositionRow(startTimeIndex);
            lesson.SetRowSpan(endTimeIndex);
            return lesson;
        }
        public void AddLessonToDays(Lesson lesson, int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo, int dayTo, int copy1Index, int copy2Index, int copy3Index)
        {
            int start = FindDay(yearFrom, monthFrom, dayFrom);
            int stop = FindDay(yearTo, monthTo, dayTo);
            lesson.SetPositionColumn(Days![start].GetDayIndex());
            for (int i = start; i <= stop; i += 7)
            {
                Days[i].Lessons.Add(lesson);
            }
            if (copy1Index != -1)
            {
                lesson.SetPositionColumn(copy1Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy1Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            Days[j].Lessons.Add(lesson);
                        }
                        break;
                    }
                }
            }
            if (copy2Index != -1)
            {
                lesson.SetPositionColumn(copy2Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy2Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            Days[j].Lessons.Add(lesson);
                        }
                        break;
                    }
                }
            }
            if (copy3Index != -1)
            {
                lesson.SetPositionColumn(copy3Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy3Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            Days[j].Lessons.Add(lesson);
                        }
                        break;
                    }
                }
            }
        }
        public void AddAllCardsToMondayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Monday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToTuesdayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Tuesday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToWednesdayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Wednesday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToThursdayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Thursday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToFridayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Friday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToSaturdayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Saturday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        public void AddAllCardsToSundayGrid(ref Grid grid)
        {
            grid.Children.Clear();
            foreach (var item in Sunday.Lessons)
            {
                grid.Children.Add(item.ConvertToCard());
            }
        }
        private int FindDay(int year, int month, int date)
        {
            int index = -1;
            for (int i = 0; i < Days!.Count; i++)
            {
                if (Days[i].IsThisDay(year, month, date))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public bool IsCurrentWeek()
        {
            DateTime now = DateTime.Now;
            return Monday.IsThisDay(now.Year, now.Month, now.Day) || Tuesday.IsThisDay(now.Year, now.Month, now.Day) || Wednesday.IsThisDay(now.Year, now.Month, now.Day) ||
                   Thursday.IsThisDay(now.Year, now.Month, now.Day) || Friday.IsThisDay(now.Year, now.Month, now.Day) || Saturday.IsThisDay(now.Year, now.Month, now.Day) ||
                   Sunday.IsThisDay(now.Year, now.Month, now.Day);
        }
    }
}

using System;
using System.Collections.ObjectModel;

namespace Schedule.Models
{
    public class Day : Notifier
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public int Index { get; set; }
        public string? ShortDayInfo { get; set; }
        public ObservableCollection<Lesson>? Lessons { get; set; }
        public string? EmptyDayKey { get; set; }

        public Day()
        {

        }
        public Day(string key)
        {
            EmptyDayKey = key;
            ShortDayInfo = string.Empty;
        }
        public Day(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Date = dateTime.Day;
            Index = SetIndex(dateTime.DayOfWeek.ToString());
            ShortDayInfo = GetShortDayInfo(dateTime);
            Lessons = new();
        }

        private string MonthToString(int month)
        {
            return month switch
            {
                1 => "January",
                2 => "February",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "",
            };
        }
        private int SetIndex(string dayOfTheWeek)
        {
            return dayOfTheWeek switch
            {
                "Monday" => 0,
                "Tuesday" => 1,
                "Wednesday" => 2,
                "Thursday" => 3,
                "Friday" => 4,
                "Saturday" => 5,
                "Sunday" => 6,
                _ => -1,
            };
        }
        private string GetShortDayInfo(DateTime dateTime)
        {
            return $"{dateTime.DayOfWeek} {MonthToString(Month)[..3]}, {Date}";
        }
        public string GetDayInfo()
        {
            if (Year == 0 && Month == 0 && Date == 0)
            {
                return "Not Available";
            }
            else
            {
                return $"{Year} {MonthToString(Month)}, {Date}";
            }
        }
        public int GetDayIndex()
        {
            return Index;
        }
        public bool IsThisDay(int year, int month, int date)
        {
            if (Year == 0 && Month == 0 && Date == 0)
            {
                return false;
            }
            else
            {
                return year == Year && month == Month && date == Date;
            }
        }
        public bool IsFuture()
        {
            if (EmptyDayKey is not null)
            {
                if (EmptyDayKey == "future")
                {
                    return true;
                }
                else return false;
            }
            else
            {
                DateTime now = DateTime.Now;
                var thisDay = new DateTime(Year, Month, Date);
                return thisDay > now;
            }
        }
        public bool IsPast()
        {
            if (EmptyDayKey is not null)
            {
                if (EmptyDayKey == "past")
                {
                    return true;
                }
                else return false;
            }
            else
            {
                DateTime now = DateTime.Now;
                var today = new DateTime(now.Year, now.Month, now.Day);
                var thisDay = new DateTime(Year, Month, Date);
                return thisDay < today;
            }
        }
        public DateTime GetDateTime()
        {
            return new DateTime(Year, Month, Date);
        }
    }
}

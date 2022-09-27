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

        public Day()
        {

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

        private string SetMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "";
            }
        }
        private int SetIndex(string dayOfTheWeek)
        {
            switch (dayOfTheWeek)
            {
                case "Monday":
                    return 0;
                case "Tuesday":
                    return 1;
                case "Wednesday":
                    return 2;
                case "Thursday":
                    return 3;
                case "Friday":
                    return 4;
                case "Saturday":
                    return 5;
                case "Sunday":
                    return 6;
                default:
                    return -1;
            }
        }
        private string GetShortDayInfo(DateTime dateTime)
        {
            return $"{dateTime.DayOfWeek} {SetMonth(Month).Substring(0, 3)}, {Date}";
        }
        public string GetDayInfo()
        {
            return $"{Year} {SetMonth(Month)}, {Date}";
        }
        public int GetDayIndex()
        {
            return Index;
        }
        public bool IsThisDay(int year, int month, int date)
        {
            return year == Year && month == Month && date == Date;
        }
        public bool IsFuture()
        {
            DateTime now = DateTime.Now;
            var thisDay = new DateTime(Year, Month, Date);
            return thisDay > now;
        }
        public bool IsPast()
        {
            DateTime now = DateTime.Now;
            var today = new DateTime(now.Year, now.Month, now.Day);
            var thisDay = new DateTime(Year, Month, Date);
            return thisDay < today;
        }

    }
}

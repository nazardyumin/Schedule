using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;

namespace Schedule.Model
{
    public class Day : Notifier
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _date;
        private readonly int _index;
        public string ShortDayInfo { get; set; }
        public ObservableCollection<Lesson> Lessons { get; set; }

        public Day(DateTime dateTime)
        {
            _year = dateTime.Year;
            _month = dateTime.Month;
            _date = dateTime.Day;
            _index = SetIndex(dateTime.DayOfWeek.ToString());
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
            return $"{dateTime.DayOfWeek} {SetMonth(_month).Substring(0,3)}, {_date}";
        }
        public string GetDayInfo()
        {
            return $"{_year} {SetMonth(_month)}, {_date}";
        }
        public int GetDayIndex()
        {
            return _index;
        }
        public bool IsCurrentDay(int year,int month, int date)
        {
            return year == _year && month == _month && date == _date;
        }
    }
}

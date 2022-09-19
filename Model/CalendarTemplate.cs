using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Model
{
    public class CalendarTemplate
    {
        public List<int>? ComboBoxYears { get; set; }
        public List<string>? ComboBoxTime { get; set; }
        public List<string>? ComboBoxDays { get; set; }
        public ObservableCollection<string>? ComboBoxMonthsFrom { get; set; }
        public ObservableCollection<int>? ComboBoxDatesFrom { get; set; }
        public ObservableCollection<string>? ComboBoxMonthsTo { get; set; }
        public ObservableCollection<int>? ComboBoxDatesTo { get; set; }

        public CalendarTemplate()
        {
            SetComboBoxYears();
            SetComboBoxTime();
            SetComboBoxDays();
            ComboBoxMonthsFrom = new();
            ComboBoxDatesFrom = new();
            ComboBoxMonthsTo = new();
            ComboBoxDatesTo = new();
        }

        private void SetComboBoxYears()
        {
            ComboBoxYears = new();
            ComboBoxYears.Add(2022);
            ComboBoxYears.Add(2023);
        }
        public string MonthToString(int month)
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
        public int MonthToInt(string month)
        {
            switch (month)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    return -1;
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
        private void SetComboBoxDays()
        {
            ComboBoxDays = new();
            ComboBoxDays.Add("Monday");
            ComboBoxDays.Add("Tuesday");
            ComboBoxDays.Add("Wednesday");
            ComboBoxDays.Add("Thursday");
            ComboBoxDays.Add("Friday");
            ComboBoxDays.Add("Saturday");
            ComboBoxDays.Add("Sunday");
        }
        public string GetSelectedDay(int year, int month, int date)
        {
            var selectedDay = new DateTime(year, month, date);
            return selectedDay.DayOfWeek.ToString();
        }
        public (int year, int month, int day, string name) GetTodayIndexesAndDay()
        {
            DateTime today = DateTime.Now;
            return (today.Year - 2022, today.Month - 1, today.Day - 1, today.DayOfWeek.ToString());
        }
        public void SetDatesFromDependOnCalendar(int year, int month)
        {
            ComboBoxDatesFrom!.Clear();
            var date = new DateTime(year, month, 1);
            for (int i = 0; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    ComboBoxDatesFrom!.Add((date + timeSpan).Day);
                }
                else break;             
            }
        }
        public void SetMonthsFromDependOnCalendar(int year)
        {
            ComboBoxMonthsFrom!.Clear();
            var date = new DateTime(year, 1, 1);
            DateTime now = DateTime.Now;
            if (year>now.Year)
            {
                for (int i=1; i <= 12; i++)
                {
                    ComboBoxMonthsFrom!.Add(MonthToString(i));
                }
            }
            else
            {
                for (int i = 0; i < 335; i++)
                {
                    var timeSpan = new TimeSpan(i, 0, 0, 0);
                    if ((date + timeSpan).Month >= now.Month)
                    {
                        if (!ComboBoxMonthsFrom.Contains(MonthToString((date + timeSpan).Month)))
                        {
                            ComboBoxMonthsFrom.Add(MonthToString((date + timeSpan).Month));
                        }
                    }
                }
            }          
        }
        public void SetDatesToDependOnCalendar(int year, int month)
        {
            ComboBoxDatesTo!.Clear();
            var date = new DateTime(year, month, 1);
            for (int i = 0; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    ComboBoxDatesTo!.Add((date + timeSpan).Day);
                }
                else break;
            }
        }
        public void SetMonthsToDependOnCalendar(int year)
        {
            ComboBoxMonthsTo!.Clear();
            var date = new DateTime(year, 1, 1);
            DateTime now = DateTime.Now;
            if (year > now.Year)
            {
                for (int i = 1; i <= 12; i++)
                {
                    ComboBoxMonthsTo!.Add(MonthToString(i));
                }
            }
            else
            {
                for (int i = 0; i < 335; i++)
                {
                    var timeSpan = new TimeSpan(i, 0, 0, 0);
                    if ((date + timeSpan).Month >= now.Month)
                    {
                        if (!ComboBoxMonthsTo!.Contains(MonthToString((date + timeSpan).Month)))
                        {
                            ComboBoxMonthsTo!.Add(MonthToString((date + timeSpan).Month));
                        }
                    }
                }
            }
        }
    }
}

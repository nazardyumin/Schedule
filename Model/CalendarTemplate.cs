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
        public List<int>? Years { get; set; }
        public List<string>? Time { get; set; }
        public List<string>? Days { get; set; }
        public ObservableCollection<string>? MonthsFrom { get; set; }
        public ObservableCollection<int>? DatesFrom { get; set; }
        public ObservableCollection<string>? MonthsTo { get; set; }
        public ObservableCollection<int>? DatesTo { get; set; }

        public CalendarTemplate()
        {
            SetComboBoxYears();
            SetComboBoxTime();
            SetComboBoxDays();
            MonthsFrom = new();
            DatesFrom = new();
            MonthsTo = new();
            DatesTo = new();
        }

        private void SetComboBoxYears()
        {
            Years = new();
            Years.Add(2022);
            Years.Add(2023);
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
            Time = new();
            Time.Add("08:00");
            Time.Add("08:10");
            Time.Add("08:20");
            Time.Add("08:30");
            Time.Add("08:40");
            Time.Add("08:50");
            Time.Add("09:00");
            Time.Add("09:10");
            Time.Add("09:20");
            Time.Add("09:30");
            Time.Add("09:40");
            Time.Add("09:50");
            Time.Add("10:00");
            Time.Add("10:10");
            Time.Add("10:20");
            Time.Add("10:30");
            Time.Add("10:40");
            Time.Add("10:50");
            Time.Add("11:00");
        }
        private void SetComboBoxDays()
        {
            Days = new();
            Days.Add("Monday");
            Days.Add("Tuesday");
            Days.Add("Wednesday");
            Days.Add("Thursday");
            Days.Add("Friday");
            Days.Add("Saturday");
            Days.Add("Sunday");
        }
        public string GetSelectedDay(int year, int month, int date)
        {
            var selectedDay = new DateTime(year, month, date);
            return selectedDay.DayOfWeek.ToString();
        }
        public (int year, int month, int day, string name) GetTodayDateAndDay()
        {
            DateTime today = DateTime.Now;
            return (today.Year, today.Month, today.Day, today.DayOfWeek.ToString());
        }
        public void SetDatesFromDependOnCalendar(int year, int month)
        {
            DatesFrom!.Clear();
            var date = new DateTime(year, month, 1);
            DateTime now = DateTime.Now;
            int i = 0;
            if (month == now.Month)
            {
                i = now.Day-1;
            }
            for (; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    
                    DatesFrom!.Add((date + timeSpan).Day);
                }
                else break;             
            }
        }
        public void SetMonthsFromDependOnCalendar(int year)
        {
            MonthsFrom!.Clear();
            var date = new DateTime(year, 1, 1);
            DateTime now = DateTime.Now;
            if (year>now.Year)
            {
                for (int i=1; i <= 12; i++)
                {
                    MonthsFrom!.Add(MonthToString(i));
                }
            }
            else
            {
                for (int i = 0; i < 335; i++)
                {
                    var timeSpan = new TimeSpan(i, 0, 0, 0);
                    if ((date + timeSpan).Month >= now.Month)
                    {
                        if (!MonthsFrom.Contains(MonthToString((date + timeSpan).Month)))
                        {
                            MonthsFrom.Add(MonthToString((date + timeSpan).Month));
                        }
                    }
                }
            }          
        }
        public void SetDatesToDependOnCalendar(int year, int month)
        {
            DatesTo!.Clear();
            var date = new DateTime(year, month, 1);
            DateTime now = DateTime.Now;
            int i = 0;
            if (month == now.Month)
            {
                i = now.Day;
            }
            for (; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    DatesTo!.Add((date + timeSpan).Day);
                }
                else break;
            }
        }
        public void SetMonthsToDependOnCalendar(int year)
        {
            MonthsTo!.Clear();
            var date = new DateTime(year, 1, 1);
            DateTime now = DateTime.Now;
            if (year > now.Year)
            {
                for (int i = 1; i <= 12; i++)
                {
                    MonthsTo!.Add(MonthToString(i));
                }
            }
            else
            {
                for (int i = 0; i < 335; i++)
                {
                    var timeSpan = new TimeSpan(i, 0, 0, 0);
                    if ((date + timeSpan).Month >= now.Month)
                    {
                        if (!MonthsTo!.Contains(MonthToString((date + timeSpan).Month)))
                        {
                            MonthsTo!.Add(MonthToString((date + timeSpan).Month));
                        }
                    }
                }
            }
        }
        public void ClearMonthsAndDates()
        {
            MonthsFrom!.Clear();
            DatesFrom!.Clear();
            MonthsTo!.Clear();
            DatesTo!.Clear();
        }
    }
}

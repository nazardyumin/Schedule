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
        public ObservableCollection<string>? StartTime { get; set; }
        public ObservableCollection<string>? EndTime { get; set; }
        //public List<string>? Days { get; set; }
        public ObservableCollection<string>? MonthsFrom { get; set; }
        public ObservableCollection<int>? DatesFrom { get; set; }
        public ObservableCollection<string>? MonthsTo { get; set; }
        public ObservableCollection<int>? DatesTo { get; set; }
        public ObservableCollection<string>? CopyDays1 { get; set; }
        public ObservableCollection<string>? CopyDays2 { get; set; }
        public ObservableCollection<string>? CopyDays3 { get; set; }
        public CalendarTemplate()
        {
            SetYears();
            SetStartTime();
            EndTime = new();
            SetCopyDays1();
            CopyDays2 = new();
            CopyDays3 = new();
            MonthsFrom = new();
            DatesFrom = new();
            MonthsTo = new();
            DatesTo = new();
        }

        private void SetYears()
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
        private void SetStartTime()
        {
            StartTime = new();
            StartTime.Add("08:00");
            StartTime.Add("08:10");
            StartTime.Add("08:20");
            StartTime.Add("08:30");
            StartTime.Add("08:40");
            StartTime.Add("08:50");
            StartTime.Add("09:00");
            StartTime.Add("09:10");
            StartTime.Add("09:20");
            StartTime.Add("09:30");
            StartTime.Add("09:40");
            StartTime.Add("09:50");
            StartTime.Add("10:00");
            StartTime.Add("10:10");
            StartTime.Add("10:20");
            StartTime.Add("10:30");
            StartTime.Add("10:40");
            StartTime.Add("10:50");
            StartTime.Add("11:00");
        }
        public void SetEndTime(int selectedIndex)
        {
            EndTime!.Clear();
            for(int i= selectedIndex+1; i<StartTime!.Count;i++)
            {
                EndTime.Add(StartTime[i]);
            }
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
        public void SetDatesFromDependOnCalendar(int year, int month, int memoryMonth)
        {
            if (month != memoryMonth)
            {
                DatesFrom!.Clear();
            }         
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
                for (int i = now.Month; i <= 12; i++)
                {
                    MonthsFrom!.Add(MonthToString(i));
                }
            }          
        }
        public void SetDatesToDependOnCalendar(int year, int month, int memoryMonth)
        {
            if (month!=memoryMonth)
            {
                DatesTo!.Clear();
            }
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
                for (int i = now.Month; i <= 12; i++)
                {
                    MonthsTo!.Add(MonthToString(i));
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
        private void SetCopyDays1()
        {
            CopyDays1 = new()
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
        }
        public void SetCopyDays2(int selectedIndex)
        {
            CopyDays2!.Clear();
            for (int i=0;i<7;i++)
            {          
                if (i!=selectedIndex) CopyDays2!.Add(CopyDays1![i]);
            }
        }
        public void SetCopyDays3(int selectedIndex)
        {
            CopyDays3!.Clear();
            for (int i = 0; i < 6; i++)
            {
                if (i != selectedIndex) CopyDays3!.Add(CopyDays2![i]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Schedule.Model
{
    public class CalendarTemplate
    {
        public List<int>? Years { get; set; }
        public ObservableCollection<string>? StartTime { get; set; }
        public ObservableCollection<string>? EndTime { get; set; }
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
            StartTime = new()
            {
                "08:00",
                "08:10",
                "08:20",
                "08:30",
                "08:40",
                "08:50",
                "09:00",
                "09:10",
                "09:20",
                "09:30",
                "09:40",
                "09:50",
                "10:00",
                "10:10",
                "10:20",
                "10:30",
                "10:40",
                "10:50",
                "11:00",
                "11:10",
                "11:20",
                "11:30",
                "11:40",
                "11:50",
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
                "13:00",
                "13:10",
                "13:20",
                "13:30",
                "13:40",
                "13:50",
                "14:00",
            };
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
            if (month == now.Month && year==now.Year)
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
        public void SetDatesToDependOnDatesFrom(int selectedDay)
        {
            DatesTo!.Clear();
            for (int i = selectedDay+1; i < DatesFrom!.Count; i++)
            {
                DatesTo!.Add(DatesFrom[i]);
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
        public void SetMonthsToDependOnMonthsFrom(int selectedMonth)
        {
            MonthsTo!.Clear();
            for (int i = selectedMonth; i<MonthsFrom!.Count;i++)
            {
                MonthsTo!.Add(MonthsFrom[i]);
            }    
        }
        public void ClearMonthsAndDates(string key)
        {
            if (key == "all")
            {
                MonthsFrom!.Clear();
                DatesFrom!.Clear();
                MonthsTo!.Clear();
                DatesTo!.Clear();
            }
            else
            {
                MonthsTo!.Clear();
                DatesTo!.Clear();
            }          
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
        private void ClearCopyDays()
        {
            CopyDays2!.Clear();
            CopyDays3!.Clear();
        }
        public int GetEndTimeIndex(string selectedValue)
        {
            switch (selectedValue)
            {
                case "08:00": return 0;
                case "08:10": return 1;
                case "08:20": return 2;
                case "08:30": return 3;
                case "08:40": return 4;
                case "08:50": return 5;
                case "09:00": return 6;
                case "09:10": return 7;
                case "09:20": return 8;
                case "09:30": return 9;
                case "09:40": return 10;
                case "09:50": return 11;
                case "10:00": return 12;
                case "10:10": return 13;
                case "10:20": return 14;
                case "10:30": return 15;
                case "10:40": return 16;
                case "10:50": return 17;
                case "11:00": return 18;
                case "11:10": return 19;
                case "11:20": return 20;
                case "11:30": return 21;
                case "11:40": return 22;
                case "11:50": return 23;
                case "12:00": return 24;
                case "12:10": return 25;
                case "12:20": return 26;
                case "12:30": return 27;
                case "12:40": return 28;
                case "12:50": return 29;
                case "13:00": return 30;
                case "13:10": return 31;
                case "13:20": return 32;
                case "13:30": return 33;
                case "13:40": return 34;
                case "13:50": return 35;
                case "14:00": return 36;








                default: return -1;
            }
        }
        public bool FromExceedsTo(int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo, int dayTo)
        {
            var from = new DateTime(yearFrom, monthFrom, dayFrom);
            var to = new DateTime(yearTo, monthTo, dayTo);
            return from > to;
        }
        public void ClearDates(string key)
        {
            if (key=="from") DatesFrom!.Clear();
            else DatesTo!.Clear();
        }
        private void ClearEndTime()
        {
            EndTime!.Clear();
        }
        public void ResetAndClear()
        {
            ClearMonthsAndDates("all");
            ClearCopyDays();
            ClearEndTime();
        }
    }
}

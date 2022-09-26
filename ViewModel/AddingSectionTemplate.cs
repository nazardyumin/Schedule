using Schedule.Model;
using Schedule.ViewModel.Bindings;
using Schedule.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;

namespace Schedule.ViewModel
{
    public class AddingSectionTemplate : Notifier
    {
        public InputBinding Subject { get; set; }
        public InputBinding Teacher { get; set; }
        public InputBinding Auditorium { get; set; }

        public ObservableCollection<string>? StartTime { get; set; }
        public ItemBinding StartTimeSelectedItem { get; set; }
        public ObservableCollection<string> EndTime { get; set; }
        public ItemBinding EndTimeSelectedItem { get; set; }

        public ObservableCollection<int>? YearsFrom { get; set; }
        public ObservableCollection<string> MonthsFrom { get; set; }
        public ObservableCollection<int> DatesFrom { get; set; }
        public ItemBinding YearsFromSelectedItem { get; set; }
        public ItemBinding MonthsFromSelectedItem { get; set; }
        public ItemBinding DatesFromSelectedItem { get; set; }

        public ObservableCollection<int> YearsTo { get; set; }
        public ObservableCollection<string> MonthsTo { get; set; }
        public ObservableCollection<int> DatesTo { get; set; }
        public ItemBinding YearsToSelectedItem { get; set; }
        public ItemBinding MonthsToSelectedItem { get; set; }
        public ItemBinding DatesToSelectedItem { get; set; }

        public ObservableCollection<string>? CopyDays1 { get; set; }
        public ObservableCollection<string> CopyDays2 { get; set; }
        public ObservableCollection<string> CopyDays3 { get; set; }
        public ItemBinding CopyDays1SelectedItem { get; set; }
        public ItemBinding CopyDays2SelectedItem { get; set; }
        public ItemBinding CopyDays3SelectedItem { get; set; }

        private string? _today;
        public string Today
        {
            get => _today!;
            set => SetField(ref _today, value);
        }

        private string? _targetDay;
        public string TargetDay
        {
            get => _targetDay!;
            set => SetField(ref _targetDay, value);
        }

        private bool _canPressAdd;
        public bool CanPressAdd
        {
            get => _canPressAdd;
            set => SetField(ref _canPressAdd, value);
        }

        private bool _canPressClear;
        public bool CanPressClear
        {
            get => _canPressClear;
            set => SetField(ref _canPressClear, value);
        }

        private bool _canPressCopy;
        public bool CanPressCopy
        {
            get => _canPressCopy;
            set => SetField(ref _canPressCopy, value);
        }

        private bool _canPressToday;
        public bool CanPressToday
        {
            get => _canPressToday;
            set => SetField(ref _canPressToday, value);
        }

        private bool _canPressTo;
        public bool CanPressTo
        {
            get => _canPressTo;
            set => SetField(ref _canPressTo, value);
        }

        private int monthFromMemory;
        private int monthToMemory;

        public MyCommand CommandToday { get; }
        public MyCommand CommandClear { get; }

        public AddingSectionTemplate()
        {
            Subject = new(RefreshStates);
            Teacher = new(RefreshStates);
            Auditorium = new(RefreshStates);

            SetStartTime();
            EndTime = new();
            StartTimeSelectedItem = new(() => { SetEndTime(); RefreshStates(); });
            EndTimeSelectedItem = new(RefreshStates);

            SetYearsFrom();
            MonthsFrom = new();
            DatesFrom = new();

            YearsTo = new();
            MonthsTo = new();
            DatesTo = new();

            YearsFromSelectedItem = new(() => { SetMonthsFrom(); RefreshStates(); });
            MonthsFromSelectedItem = new(() => { SetDatesFrom(); RefreshStates(); });
            DatesFromSelectedItem = new(() => { ChangeToday(); RefreshStates(); });

            YearsToSelectedItem = new(() => { SetMonthsTo(); RefreshStates(); });
            MonthsToSelectedItem = new(() => { SetDatesTo(); RefreshStates(); });
            DatesToSelectedItem = new(() => { ChangeTargetDay(); RefreshStates(); });

            CopyDays1 = new();
            CopyDays2 = new();
            CopyDays3 = new();

            CopyDays1SelectedItem = new(() => { SetCopyDays2(); RefreshStates(); });
            CopyDays2SelectedItem = new(() => { SetCopyDays3(); RefreshStates(); });
            CopyDays3SelectedItem = new(RefreshStates);

            Today = "Today";
            TargetDay = string.Empty;
            monthFromMemory = 0;
            monthToMemory = 0;

            CommandToday = new(_ => { CommandTodayFunction(); RefreshStates(); }, _ => true);
            CommandClear = new(_ => { CommandClearFunction(); RefreshStates(); }, _ => true);

            CanPressToday = true;
            CanPressAdd = false;
            CanPressClear = false;
            CanPressCopy = false;
            CanPressTo = false;
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
        private void SetEndTime()
        {
            EndTime!.Clear();
            if (StartTimeSelectedItem.Index>=0)
            {
                for (int i = StartTimeSelectedItem.Index + 1; i < StartTime!.Count; i++)
                {
                    EndTime.Add(StartTime[i]);
                }
            }    
        }

        private void SetYearsFrom()
        {
            YearsFrom = new()
            {
                2022,
                2023,
                2024
            };
        }
        private void SetMonthsFrom()
        {
            if (YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk &&
                YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk)
            {
                var check = FromExceedsTo();
                if (check)
                {
                    YearsToSelectedItem.Index = -1;
                    ClearMonthsAndDates("to");
                    TargetDay = string.Empty;
                }
            }
            if (YearsFromSelectedItem.IsOk)
            {
                SetYearsTo();
                SetMonthsFromDependOnCalendar();
                ClearDates("from");
                Today = "Today";
            }
        }
        private void SetMonthsFromDependOnCalendar()
        {
            MonthsFrom!.Clear();
            DateTime now = DateTime.Now;
            if (YearsFromSelectedItem.ValueToInt() > now.Year)
            {
                for (int i = 1; i <= 12; i++)
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
        private void SetDatesFrom()
        {
            if (YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk &&
                YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk)
            {
                var check = FromExceedsTo();
                if (check)
                {
                    YearsToSelectedItem.Index = -1;
                    ClearMonthsAndDates("to");
                    TargetDay = string.Empty;
                }
            }
            if (YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk)
            {
                SetDatesFromDependOnCalendar();
                monthFromMemory = MonthToInt(MonthsFromSelectedItem.Value);
                Today = "Today";
            }
        }
        private void SetDatesFromDependOnCalendar()
        {
            var year = YearsFromSelectedItem.ValueToInt();
            var month = MonthToInt(MonthsFromSelectedItem.Value);
            if (month != monthFromMemory)
            {
                DatesFrom!.Clear();
            }
            var date = new DateTime(year, month, 1);
            DateTime now = DateTime.Now;
            int i = 0;
            if (month == now.Month)
            {
                i = now.Day - 1;
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

        private void SetYearsTo()
        {
            YearsTo!.Clear();
            if (YearsFromSelectedItem.Index>-1)
            {
                for (int i = YearsFromSelectedItem.Index; i < YearsFrom!.Count; i++)
                {
                    YearsTo.Add(YearsFrom[i]);
                }
                YearsToSelectedItem.Index = -1;
            }         
        }
        private void SetMonthsTo()
        {
            if (YearsToSelectedItem.IsOk)
            {
                ClearMonthsAndDates("to");
                TargetDay = string.Empty;
                if (YearsFromSelectedItem.Value == YearsToSelectedItem.Value)
                {
                    SetMonthsToDependOnMonthsFrom();
                }
                else
                {
                    SetMonthsToDependOnCalendar();
                }
            }
        }
        private void SetMonthsToDependOnCalendar()
        {
            MonthsTo!.Clear();
            var year = YearsToSelectedItem.ValueToInt();
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
        private void SetMonthsToDependOnMonthsFrom()
        {
            MonthsTo!.Clear();
            for (int i = MonthsFromSelectedItem.Index; i < MonthsFrom!.Count; i++)
            {
                MonthsTo!.Add(MonthsFrom[i]);
            }
        }

        private void SetDatesTo()
        {
            if (YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk)
            {
                ClearDates("to");
                TargetDay = string.Empty;
                if (YearsFromSelectedItem.Value == YearsToSelectedItem.Value && MonthsFromSelectedItem.Value == MonthsToSelectedItem.Value)
                {
                    SetDatesToDependOnDatesFrom();
                }
                else
                {
                    SetDatesToDependOnCalendar();
                }

                monthToMemory = MonthToInt(MonthsToSelectedItem.Value);
                SetCopyDays1();
            }
        }
        private void SetDatesToDependOnCalendar()
        {
            var year = YearsToSelectedItem.ValueToInt();
            var month = MonthToInt(MonthsToSelectedItem.Value);
            if (month != monthToMemory)
            {
                DatesTo!.Clear();
            }
            var date = new DateTime(year, month, 1);
            DateTime now = DateTime.Now;
            int i = 0;
            if (month == now.Month && year == now.Year)
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
        private void SetDatesToDependOnDatesFrom()
        {
            DatesTo!.Clear();
            for (int i = DatesFromSelectedItem.Index + 1; i < DatesFrom!.Count; i++)
            {
                DatesTo!.Add(DatesFrom[i]);
            }
        }

        private void SetCopyDays1()
        {
            CopyDays1!.Clear();
            if (Today != "Monday") CopyDays1!.Add("Monday");
            if (Today != "Tuesday") CopyDays1!.Add("Tuedsay");
            if (Today != "Wednesday") CopyDays1!.Add("Wednesday");
            if (Today != "Thursday") CopyDays1!.Add("Thursday");
            if (Today != "Friday") CopyDays1!.Add("Friday");
            if (Today != "Saturday") CopyDays1!.Add("Saturday");
            if (Today != "Sunday") CopyDays1!.Add("Sunday");
        }
        private void SetCopyDays2()
        {
            CopyDays2!.Clear();
            for (int i = 0; i < 6; i++)
            {
                if (i != CopyDays1SelectedItem.Index) CopyDays2!.Add(CopyDays1![i]);
            }
        }
        private void SetCopyDays3()
        {
            CopyDays3!.Clear();
            if (CopyDays2SelectedItem.IsOk)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i != CopyDays2SelectedItem.Index) CopyDays3!.Add(CopyDays2![i]);
                }
            }    
        }

        private void ClearYearsTo()
        {
            YearsTo!.Clear();
        }
        private void ClearMonthsAndDates(string key)
        {
            if (key == "all")
            {
                DatesTo!.Clear();
                DatesFrom!.Clear();
                MonthsTo!.Clear();
                MonthsFrom!.Clear();
            }
            else
            {
                DatesTo!.Clear();
                MonthsTo!.Clear();
            }
        }
        private void ClearDates(string key)
        {
            if (key == "from") DatesFrom!.Clear();
            else DatesTo!.Clear();
        }
        private void ClearCopyDays()
        {
            CopyDays2!.Clear();
            CopyDays3!.Clear();
        }
        private void ResetAndClear()
        {
            ClearMonthsAndDates("all");
            ClearYearsTo();
            ClearCopyDays();
        }

        private void ChangeToday()
        {
            if (YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk)
            {
                Today = GetSelectedDay(YearsFromSelectedItem.ValueToInt(), MonthToInt(MonthsFromSelectedItem.Value), DatesFromSelectedItem.ValueToInt());
                CanPressToday = false;
            }
        }
        private void ChangeTargetDay()
        {
            if (YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk)
            {
                TargetDay = GetSelectedDay(YearsToSelectedItem.ValueToInt(), MonthToInt(MonthsToSelectedItem.Value), DatesToSelectedItem.ValueToInt());
            }
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
        private int MonthToInt(string month)
        {
            return month switch
            {
                "January" => 1,
                "February" => 2,
                "March" => 3,
                "April" => 4,
                "May" => 5,
                "June" => 6,
                "July" => 7,
                "August" => 8,
                "September" => 9,
                "October" => 10,
                "November" => 11,
                "December" => 12,
                _ => -1,
            };
        }
        private int CopyDayToIndex(string dayOfTheWeek)
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

        private int GetEndTimeIndex(string selectedValue)
        {
            return selectedValue switch
            {
                "08:00" => 0,
                "08:10" => 1,
                "08:20" => 2,
                "08:30" => 3,
                "08:40" => 4,
                "08:50" => 5,
                "09:00" => 6,
                "09:10" => 7,
                "09:20" => 8,
                "09:30" => 9,
                "09:40" => 10,
                "09:50" => 11,
                "10:00" => 12,
                "10:10" => 13,
                "10:20" => 14,
                "10:30" => 15,
                "10:40" => 16,
                "10:50" => 17,
                "11:00" => 18,
                "11:10" => 19,
                "11:20" => 20,
                "11:30" => 21,
                "11:40" => 22,
                "11:50" => 23,
                "12:00" => 24,
                "12:10" => 25,
                "12:20" => 26,
                "12:30" => 27,
                "12:40" => 28,
                "12:50" => 29,
                "13:00" => 30,
                "13:10" => 31,
                "13:20" => 32,
                "13:30" => 33,
                "13:40" => 34,
                "13:50" => 35,
                "14:00" => 36,
                _ => -1,
            };
        }
        private (int year, int month, int day, string name) GetTodayDateAndDay()
        {
            DateTime today = DateTime.Now;
            return (today.Year, today.Month, today.Day, today.DayOfWeek.ToString());
        }
        private string GetSelectedDay(int year, int month, int date)
        {
            var selectedDay = new DateTime(year, month, date);
            return selectedDay.DayOfWeek.ToString();
        }
        private bool FromExceedsTo()
        {
            var monthFrom = MonthToInt(MonthsFromSelectedItem.Value);
            var monthTo = MonthToInt(MonthsToSelectedItem.Value);
            var from = new DateTime(YearsFromSelectedItem.ValueToInt(), monthFrom, DatesFromSelectedItem.ValueToInt());
            var to = new DateTime(YearsToSelectedItem.ValueToInt(), monthTo, DatesToSelectedItem.ValueToInt());
            return from > to;
        }

        private void RefreshCanAddState()
        {
            if (Subject.IsOk && Teacher.IsOk && Auditorium.IsOk &&
                StartTimeSelectedItem.IsOk && EndTimeSelectedItem.IsOk &&
                YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk &&
                YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk)
            {
                CanPressAdd = true;
            }
            else if (Subject.IsOk && Teacher.IsOk && Auditorium.IsOk &&
                StartTimeSelectedItem.IsOk && EndTimeSelectedItem.IsOk &&
                YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk &&
                !YearsToSelectedItem.IsOk && !MonthsToSelectedItem.IsOk && !DatesToSelectedItem.IsOk)
            {
                CanPressAdd = true;
            }
            else CanPressAdd = false;
        }
        private void RefreshCanClearState()
        {
            if (Subject.IsOk || Teacher.IsOk || Auditorium.IsOk || StartTimeSelectedItem.IsOk || YearsFromSelectedItem.IsOk || YearsToSelectedItem.IsOk || CopyDays1SelectedItem.IsOk)
            {
                CanPressClear = true;
            }
            else
            {
                CanPressClear = false;
            }
        }
        private void RefreshCanCopyState()
        {
            if (Subject.IsOk && Teacher.IsOk && Auditorium.IsOk &&
                StartTimeSelectedItem.IsOk && EndTimeSelectedItem.IsOk &&
                YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk &&
                YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk)
            {
                CanPressCopy = true;
            }
            else
            {
                CanPressCopy = false;
            }
        }
        private void RefreshToState()
        {
            if (Subject.IsOk && Teacher.IsOk && Auditorium.IsOk &&
                            StartTimeSelectedItem.IsOk && EndTimeSelectedItem.IsOk &&
                            YearsFromSelectedItem.IsOk && MonthsFromSelectedItem.IsOk && DatesFromSelectedItem.IsOk)
            {
                CanPressTo = true;
            }
            else
            {
                CanPressTo = false;
            }
        }
        private void RefreshStates()
        {
            RefreshCanAddState();
            RefreshCanClearState();
            RefreshCanCopyState();
            RefreshToState();
        }

        private void CommandTodayFunction()
        {
            (int year, int month, int day, string name) = GetTodayDateAndDay();
            YearsFromSelectedItem.Index = YearsFrom!.IndexOf(year);
            SetYearsTo();
            SetMonthsFromDependOnCalendar();
            MonthsFromSelectedItem.Index = MonthsFrom!.IndexOf(MonthToString(month));
            SetDatesFromDependOnCalendar();
            monthFromMemory = MonthToInt(MonthsFromSelectedItem.Value);
            DatesFromSelectedItem.Index = DatesFrom!.IndexOf(day);
            Today = name;
            CanPressToday = false;
        }
        public void CommandClearFunction()
        {
            Subject.Value = Teacher.Value = Auditorium.Value = string.Empty;
            ResetAndClear();
            StartTimeSelectedItem.Index = -1;
            YearsFromSelectedItem.Index = -1;
            Today = "Today";
            TargetDay = string.Empty;
            monthFromMemory = 0;
            monthToMemory = 0;
            CopyDays1SelectedItem.Index = -1;
            ClearCopyDays();
            CanPressToday = true;
            CanPressAdd = CanPressClear = CanPressCopy = CanPressTo = false;
        }

        public (int year, int month, int day) GetFromValues()
        {
            return (YearsFromSelectedItem.ValueToInt(), MonthToInt(MonthsFromSelectedItem.Value), DatesFromSelectedItem.ValueToInt());
        }
        public (int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo, int dayTo, int copy1, int copy2, int copy3) GetFromAndToValues()
        {
            return (YearsFromSelectedItem.ValueToInt(), MonthToInt(MonthsFromSelectedItem.Value), DatesFromSelectedItem.ValueToInt(),
                    YearsToSelectedItem.ValueToInt(), MonthToInt(MonthsToSelectedItem.Value), DatesToSelectedItem.ValueToInt(),
                    CopyDayToIndex(CopyDays1SelectedItem.Value), CopyDayToIndex(CopyDays2SelectedItem.Value), CopyDayToIndex(CopyDays3SelectedItem.Value));
        }
        public (string subject, string teacher, string auditorium, int startTimeIndex, int endTimeIndex, string duration) GetSetupInfo()
        {
            return (Subject.Value, Teacher.Value, Auditorium.Value,
                    StartTimeSelectedItem.Index, GetEndTimeIndex(EndTimeSelectedItem.Value),
                    $"{StartTimeSelectedItem.Value} - {EndTimeSelectedItem.Value}");
        }
        public bool IsPeriodSelected()
        {
            return YearsToSelectedItem.IsOk && MonthsToSelectedItem.IsOk && DatesToSelectedItem.IsOk;
        }
    }
}

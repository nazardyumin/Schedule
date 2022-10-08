using Schedule.Models;
using Schedule.ViewModels.AddingSection;
using Schedule.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;

namespace Schedule.ViewModels
{
    public class AddingSectionTemplate : Notifier
    {
        public ObservableCollection<string>? StartTime { get; set; }
        public ObservableCollection<string> EndTime { get; set; }
        public ObservableCollection<int>? YearsFrom { get; set; }
        public ObservableCollection<string> MonthsFrom { get; set; }
        public ObservableCollection<int> DatesFrom { get; set; }
        public ObservableCollection<int> YearsTo { get; set; }
        public ObservableCollection<string> MonthsTo { get; set; }
        public ObservableCollection<int> DatesTo { get; set; }
        public ObservableCollection<string>? CopyDays1 { get; set; }
        public ObservableCollection<string> CopyDays2 { get; set; }
        public ObservableCollection<string> CopyDays3 { get; set; }
        public AddingSectionDataBinding Data { get; set; }
        public AddingSectionControlsContentBinding Content { get; set; }
        public AddingSectionStateBinding States { get; set; }

        private int _monthFromMemory;
        private int _monthToMemory;

        private int _connectionDayIndex;
        private int _connectionLessonIndex;

        public MyCommand CommandToday { get; }
        public MyCommand CommandClear { get; }
        public MyCommand CommandCancel { get; }

        public AddingSectionTemplate()
        {
            Data = new();
            Data.Subject = new(RefreshStates);
            Data.Teacher = new(RefreshStates);
            Data.Auditorium = new(RefreshStates);

            SetStartTime();
            EndTime = new();
            Data.StartTimeSelectedItem = new(() =>
            {
                SetEndTime();
                RefreshStates();
            });
            Data.EndTimeSelectedItem = new(RefreshStates);

            SetYearsFrom();
            MonthsFrom = new();
            DatesFrom = new();

            YearsTo = new();
            MonthsTo = new();
            DatesTo = new();

            Data.YearsFromSelectedItem = new(() =>
            {
                SetMonthsFrom();
                RefreshStates();
            });
            Data.MonthsFromSelectedItem = new(() =>
            {
                SetDatesFrom();
                RefreshStates();
            });
            Data.DatesFromSelectedItem = new(() =>
            {
                ChangeToday();
                RefreshStates();
            });

            Data.YearsToSelectedItem = new(() =>
            {
                SetMonthsTo();
                RefreshStates();
            });
            Data.MonthsToSelectedItem = new(() =>
            {
                SetDatesTo();
                RefreshStates();
            });
            Data.DatesToSelectedItem = new(() =>
            {
                ChangeTargetDay();
                RefreshStates();
            });

            CopyDays1 = new();
            CopyDays2 = new();
            CopyDays3 = new();

            Data.CopyDays1SelectedItem = new(() =>
            {
                SetCopyDays2();
                RefreshStates();
            });
            Data.CopyDays2SelectedItem = new(() =>
            {
                SetCopyDays3();
                RefreshStates();
            });
            Data.CopyDays3SelectedItem = new(RefreshStates);

            Content = new();

            _monthFromMemory = 0;
            _monthToMemory = 0;

            CommandToday = new(_ =>
            {
                CommandTodayFunction();
                RefreshStates();
            }, _ => true);
            CommandClear = new(_ =>
            {
                CommandClearFunction();
                RefreshStates();
            }, _ => true);
            CommandCancel = new(_ =>
            {
                CommandCancelFunction();
                RefreshStates();
            }, _ => true);

            States = new();
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
                "14:10",
                "14:20",
                "14:30",
                "14:40",
                "14:50",
                "15:00",
                "15:10",
                "15:20",
                "15:30",
                "15:40",
                "15:50",
                "16:00",
                "16:10",
                "16:20",
                "16:30",
                "16:40",
                "16:50",
                "17:00",
                "17:10",
                "17:20",
                "17:30",
                "17:40",
                "17:50",
                "18:00",
                "18:10",
                "18:20",
                "18:30",
                "18:40",
                "18:50",
                "19:00",
                "19:10",
                "19:20",
                "19:30",
                "19:40",
                "19:50",
                "20:00",
            };
        }

        private void SetEndTime()
        {
            EndTime.Clear();
            if (Data.StartTimeSelectedItem!.Index < 0) return;
            for (var i = Data.StartTimeSelectedItem.Index + 1; i < StartTime!.Count; i++)
            {
                EndTime.Add(StartTime[i]);
            }
        }

        private void SetYearsFrom()
        {
            YearsFrom = Configurator.Load().Years;
        }

        private void SetMonthsFrom()
        {
            if (Data.AllDatesFromAndToSelected())
            {
                var check = FromExceedsTo();
                if (check)
                {
                    Data.YearsToSelectedItem!.Index = -1;
                    ClearMonthsAndDates("to");
                    Content.ResetTargetDay();
                }
            }

            if (Data.YearFromNotSelected()) return;
            SetYearsTo();
            SetMonthsFromDependOnCalendar();
            ClearDates("from");
            Content.ResetToday();
        }

        private void SetMonthsFromDependOnCalendar()
        {
            MonthsFrom.Clear();
            var now = DateTime.Now;
            if (Data.GetYearFrom() > now.Year)
            {
                for (var i = 1; i <= 12; i++)
                {
                    MonthsFrom.Add(MonthToString(i));
                }
            }
            else
            {
                for (var i = now.Month; i <= 12; i++)
                {
                    MonthsFrom.Add(MonthToString(i));
                }
            }
        }

        private void SetDatesFrom()
        {
            if (Data.AllDatesFromAndToSelected())
            {
                var check = FromExceedsTo();
                if (check)
                {
                    Data.YearsToSelectedItem!.Index = -1;
                    ClearMonthsAndDates("to");
                    Content.ResetTargetDay();
                }
            }

            if (Data.YearFromOrMonthFromNotSelected()) return;
            SetDatesFromDependOnCalendar();
            _monthFromMemory = Data.GetMonthFrom();
            Content.ResetToday();
        }

        private void SetDatesFromDependOnCalendar()
        {
            var year = Data.GetYearFrom();
            var month = Data.GetMonthFrom();
            if (month != _monthFromMemory)
            {
                DatesFrom.Clear();
            }

            var date = new DateTime(year, month, 1);
            var now = DateTime.Now;
            var i = 0;
            if (month == now.Month)
            {
                i = now.Day - 1;
            }

            for (; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    DatesFrom.Add((date + timeSpan).Day);
                }
                else break;
            }
        }

        private void SetYearsTo()
        {
            YearsTo.Clear();
            if (Data.YearFromNotSelected()) return;
            for (var i = Data.YearsFromSelectedItem!.Index; i < YearsFrom!.Count; i++)
            {
                YearsTo.Add(YearsFrom[i]);
            }
            Data.YearsToSelectedItem!.Index = -1;
        }

        private void SetMonthsTo()
        {
            if (Data.YearToNotSelected()) return;
            ClearMonthsAndDates("to");
            Content.ResetTargetDay();
            if (Data.YearFromEqualsYearTo())
            {
                SetMonthsToDependOnMonthsFrom();
            }
            else
            {
                SetMonthsToDependOnCalendar();
            }
        }

        private void SetMonthsToDependOnCalendar()
        {
            MonthsTo.Clear();
            var year = Data.GetYearTo();
            var now = DateTime.Now;
            if (year > now.Year)
            {
                for (var i = 1; i <= 12; i++)
                {
                    MonthsTo.Add(MonthToString(i));
                }
            }
            else
            {
                for (var i = now.Month; i <= 12; i++)
                {
                    MonthsTo.Add(MonthToString(i));
                }
            }
        }

        private void SetMonthsToDependOnMonthsFrom()
        {
            MonthsTo.Clear();
            for (var i = Data.MonthsFromSelectedItem!.Index; i < MonthsFrom.Count; i++)
            {
                MonthsTo.Add(MonthsFrom[i]);
            }
        }

        private void SetDatesTo()
        {
            if (Data.YearToOrMonthToNotSelected()) return;
            ClearDates("to");
            Content.ResetTargetDay();
            if (Data.MonthFromEqualsMonthTo())
            {
                SetDatesToDependOnDatesFrom();
            }
            else
            {
                SetDatesToDependOnCalendar();
            }

            _monthToMemory = Data.GetMonthTo();
            SetCopyDays1();
        }

        private void SetDatesToDependOnCalendar()
        {
            var year = Data.GetYearTo();
            var month = Data.GetMonthTo();
            if (month != _monthToMemory)
            {
                DatesTo.Clear();
            }

            var date = new DateTime(year, month, 1);
            var now = DateTime.Now;
            var i = 0;
            if (month == now.Month && year == now.Year)
            {
                i = now.Day;
            }

            for (; i < 31; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                if ((date + timeSpan).Month == month)
                {
                    DatesTo.Add((date + timeSpan).Day);
                }
                else break;
            }
        }

        private void SetDatesToDependOnDatesFrom()
        {
            DatesTo.Clear();
            for (var i = Data.DatesFromSelectedItem!.Index + 1; i < DatesFrom.Count; i++)
            {
                DatesTo.Add(DatesFrom[i]);
            }
        }

        private void SetCopyDays1()
        {
            CopyDays1!.Clear();
            if (Content.Today != "Monday") CopyDays1!.Add("Monday");
            if (Content.Today != "Tuesday") CopyDays1!.Add("Tuesday");
            if (Content.Today != "Wednesday") CopyDays1!.Add("Wednesday");
            if (Content.Today != "Thursday") CopyDays1!.Add("Thursday");
            if (Content.Today != "Friday") CopyDays1!.Add("Friday");
            if (Content.Today != "Saturday") CopyDays1!.Add("Saturday");
            if (Content.Today != "Sunday") CopyDays1!.Add("Sunday");
        }

        private void SetCopyDays2()
        {
            CopyDays2.Clear();
            foreach (var day in CopyDays1!)
            {
                if (day == Content.Today || day == Data.GetCopyDay1()) continue;
                CopyDays2.Add(day);
            }
        }

        private void SetCopyDays3()
        {
            CopyDays3.Clear();
            foreach (var day in CopyDays2)
            {
                if (day == Content.Today || day == Data.GetCopyDay1() || day == Data.GetCopyDay2()) continue;
                CopyDays3.Add(day);
            }
        }

        private void ClearYearsTo()
        {
            YearsTo.Clear();
        }

        private void ClearMonthsAndDates(string key)
        {
            if (key == "all")
            {
                DatesTo.Clear();
                DatesFrom.Clear();
                MonthsTo.Clear();
                MonthsFrom.Clear();
            }
            else
            {
                DatesTo.Clear();
                MonthsTo.Clear();
            }
        }

        private void ClearDates(string key)
        {
            if (key == "from") DatesFrom.Clear();
            else DatesTo.Clear();
        }

        private void ClearCopyDays()
        {
            CopyDays2.Clear();
            CopyDays3.Clear();
        }

        private void ResetAndClear()
        {
            ClearMonthsAndDates("all");
            ClearYearsTo();
            ClearCopyDays();
        }

        public void ClearTime()
        {
            Data.StartTimeSelectedItem!.Value = string.Empty;
            EndTime.Clear();
        }

        private void ChangeToday()
        {
            if (Data.AnyDateFromNotSelected()) return;
            Content.Today = GetSelectedDay(Data.GetYearFrom(), Data.GetMonthFrom(), Data.GetDateFrom());
            States.CanPressToday = false;
        }

        private void ChangeTargetDay()
        {
            if (Data.AllDatesToSelected())
            {
                Content.TargetDay = GetSelectedDay(Data.GetYearTo(), Data.GetMonthTo(), Data.GetDateTo());
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

        private (int year, int month, int day, string name) GetTodayDateAndDay()
        {
            var today = DateTime.Now;
            return (today.Year, today.Month, today.Day, today.DayOfWeek.ToString());
        }

        private string GetSelectedDay(int year, int month, int date)
        {
            var selectedDay = new DateTime(year, month, date);
            return selectedDay.DayOfWeek.ToString();
        }

        private bool FromExceedsTo()
        {
            var from = new DateTime(Data.GetYearFrom(), Data.GetMonthFrom(), Data.GetDateFrom());
            var to = new DateTime(Data.GetYearTo(), Data.GetMonthTo(), Data.GetDateTo());
            return from > to;
        }

        private void RefreshCanAddState()
        {
            if (Data.AnyFieldIsEmpty())
            {
                if (Data.AllFieldsSelectedAndPeriodNotSelected())
                {
                    States.CanPressAdd = true;
                }
                else States.CanPressAdd = false;
            }
            else
            {
                States.CanPressAdd = true;
            }
        }

        private void RefreshCanClearState()
        {
            if (Data.AnyFieldIsNotEmpty())
            {
                States.CanPressClear = true;
            }
            else
            {
                States.CanPressClear = false;
            }
        }

        private void RefreshCanCopyState()
        {
            if (Data.AnyFieldIsEmpty())
            {
                States.CanPressCopy = false;
            }
            else
            {
                States.CanPressCopy = true;
            }
        }

        private void RefreshToAndSaveState()
        {
            if (Data.AllFieldsExceptPeriodSelected())
            {
                States.CanPressTo = true;
                States.CanPressSave = true;
            }
            else
            {
                States.CanPressTo = false;
                States.CanPressSave = false;
            }
        }

        private void RefreshStates()
        {
            RefreshCanAddState();
            RefreshCanClearState();
            RefreshCanCopyState();
            RefreshToAndSaveState();
        }

        private void CommandTodayFunction()
        {
            var (year, month, day, name) = GetTodayDateAndDay();
            Data.YearsFromSelectedItem!.Index = YearsFrom!.IndexOf(year);
            SetYearsTo();
            SetMonthsFromDependOnCalendar();
            Data.MonthsFromSelectedItem!.Index = MonthsFrom.IndexOf(MonthToString(month));
            _monthFromMemory = Data.GetMonthFrom();
            Data.DatesFromSelectedItem!.Index = DatesFrom.IndexOf(day);
            Content.Today = name;
            States.CanPressToday = false;
        }

        public void CommandClearFunction()
        {
            Data.ClearInputs();
            ResetAndClear();
            Data.StartTimeSelectedItem!.Index = -1;
            Data.YearsFromSelectedItem!.Index = -1;
            Content.ResetToday();
            Content.ResetTargetDay();
            _monthFromMemory = 0;
            _monthToMemory = 0;
            Data.CopyDays1SelectedItem!.Index = -1;
            ClearCopyDays();
            States.CanPressToday = true;
            States.CanPressAdd = States.CanPressClear = States.CanPressCopy = States.CanPressTo = false;
        }

        public void CommandCancelFunction()
        {
            Content.AddOrSave = "Add";
            States.IsAddingMode = true;
            CommandClearFunction();
        }

        public (string subject, string teacher, string auditorium, int startTimeIndex, int endTimeIndex, string duration, DateTime date, int dayIndex, int lessonIndex) GetSetupInfo()
        {
            return (Data.GetSubject(), Data.GetTeacher(), Data.GetAuditorium(),
                    Data.GetStartTimeIndex(), Data.GetEndTimeIndex(),
                    Data.GetDuration(), new DateTime(Data.GetYearFrom(), Data.GetMonthFrom(), Data.GetDateFrom()),
                    _connectionDayIndex, _connectionLessonIndex);
        }

        public void Editor(Lesson lesson)
        {
            _connectionDayIndex = lesson.ConnectionDayIndex;
            _connectionLessonIndex = lesson.ConnectionLessonIndex;
            Content.AddOrSave = "Save";
            States.IsAddingMode = false;
            Data.Subject!.Value = lesson.Subject;
            Data.Teacher!.Value = lesson.Teacher;
            Data.Auditorium!.Value = lesson.Auditorium;
            var start = lesson.Duration[..5];
            var end = lesson.Duration[8..];
            Data.StartTimeSelectedItem!.Value = start;
            Data.EndTimeSelectedItem!.Value = end;
            SetYearsFrom();
            Data.YearsFromSelectedItem!.Index = YearsFrom!.IndexOf(lesson.Date.Year);
            SetMonthsFrom();
            Data.MonthsFromSelectedItem!.Index = MonthsFrom.IndexOf(MonthToString(lesson.Date.Month));
            SetDatesFrom();
            Data.DatesFromSelectedItem!.Index = DatesFrom.IndexOf(lesson.Date.Day);
            ChangeToday();
        }
    }
}
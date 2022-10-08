using Schedule.Models;
using Schedule.ViewModels.Bindings;
using Schedule.ViewModels.Week;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Schedule.ViewModels
{
    public class WeekTemplate : Notifier
    {
        private List<Day>? _days;
        public Day[] Week { get; set; }
        public WeekDayInfoBinding DayInfo { get; set; }
        public WeekStateBinding States { get; set; }

        private string? _header;
        public string Header
        {
            get => _header!;
            set => SetField(ref _header, value);
        }

        private int _iterator;
        private int _currentDayIndex;
        private readonly int _lastIndex;

        public WeekTemplate()
        {
            SetDays();
            _lastIndex = _days!.Count - 1;
            Week = new Day[7];
            DayInfo = new();  
            States = new();
            FocusOnCurrentWeek();
            _iterator = _days.IndexOf(Week[0]);
        }

        private void SetDays()
        {
            _days = Serializer.Load();
            var setup = Configurator.Load();
            var years = setup.Years;
            if (_days.Count == 0)
            {
                SetDaysWithoutFile(setup.DaysCount, years!);
            }
            else
            {
                AddDaysIfYearsChanged(years!);
            }
        }

        private void SetDaysWithoutFile(int count, ObservableCollection<int> years)
        {
            var firstDay = new DateTime(years[0], 1, 1);
            for (var i = 0; i <= count; i++)
            {
                var timeSpan = new TimeSpan(i, 0, 0, 0);
                _days!.Add(new Day(firstDay + timeSpan));
            }

            Serializer.Save(_days!);
        }

        private void AddDaysIfYearsChanged(ObservableCollection<int> years)
        {
            foreach (var year in years)
            {
                if (year <= _days![^1].Year) continue;
                var firstDay = new DateTime(year, 1, 1);
                var lastDay = new DateTime(years[^1], 12, 31);
                var result = lastDay - firstDay;
                for (var i = 0; i <= result.TotalDays; i++)
                {
                    var timeSpan = new TimeSpan(i, 0, 0, 0);
                    _days.Add(new Day(firstDay + timeSpan));
                }

                Serializer.Save(_days);
                break;
            }
        }

        private void FocusOnCurrentWeek()
        {
            var currentDate = DateTime.Now;
            var currentDayOfTheWeek = 0;
            foreach (var item in from item in _days!
                                 where item.IsThisDay(currentDate.Year, currentDate.Month, currentDate.Day)
                                 select item)
            {
                var index = _days!.IndexOf(item);
                currentDayOfTheWeek = _days[index].GetDayIndex();
                TodayIs(currentDayOfTheWeek, index);
            }

            _currentDayIndex = currentDayOfTheWeek;
        }

        private void TodayIs(int currentDayOfTheWeek, int index)
        {
            var j = 0 - currentDayOfTheWeek;
            for (var i = 0; i < 7; i++)
            {
                if (j < 0) Week[i] = index + j >= 0 ? _days![index + j] : new Day("past");
                else if (j == 0) Week[i] = _days![index + j];
                else Week[i] = index + j <= _lastIndex ? _days![index + j] : new Day("future");
                j++;
            }
            Header = SetHeader();
            SetShortDayInfo();
        }

        private string SetHeader()
        {
            return $"{Week[0].GetDayInfo()} - {Week[6].GetDayInfo()}";
        }

        private void SetShortDayInfo()
        {
            DayInfo!.Set(new string[] {
                Week[0].ShortDayInfo!,
                Week[1].ShortDayInfo!,
                Week[2].ShortDayInfo!,
                Week[3].ShortDayInfo!,
                Week[4].ShortDayInfo!,
                Week[5].ShortDayInfo!,
                Week[6].ShortDayInfo! });
        }

        private int FindDay(int year, int month, int date)
        {
            var index = -1;
            for (var i = 0; i < _days!.Count; i++)
            {
                if (!_days[i].IsThisDay(year, month, date)) continue;
                index = i;
                break;
            }

            return index;
        }

        public int GetCurrentDayIndex()
        {
            return _currentDayIndex;
        }

        public (bool isCurrentWeek, bool isFuture, int index) NextWeek()
        {
            States.StepsFromCurrentWeek++;
            var now = DateTime.Now;
            PlusWeek();
            States.CanPressForward = _iterator + 6 < _lastIndex;
            States.CanPressBack = _iterator - 1 > 0;
            for (var i = 0; i < 7; i++)
            {
                if (Week[i].IsThisDay(now.Year, now.Month, now.Day)) return (true, false, i);
            }
            return Week[0].IsFuture() ? (false, true, 0) : (false, false, -1);
        }

        private void PlusWeek()
        {
            if (_iterator >= 0)
            {
                PlusWeekIteratorNotNull();
            }
            else
            {
                PlusWeekIteratorNull();
            }

            Header = SetHeader();
            SetShortDayInfo();
        }

        private void PlusWeekIteratorNotNull()
        {
            for (var i = 0; i < 7; i++)
            {
                Week[i] = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
                _iterator++;
            }
        }

        private void PlusWeekIteratorNull()
        {
            for (var i = 0; i < 7; i++)
            {
                Week[i] = _days![_iterator + 7];
                _iterator++;
            }
        }

        public (bool isCurrentWeek, bool isFuture, int index) PreviousWeek()
        {
            States.StepsFromCurrentWeek--;
            var now = DateTime.Now;
            MinusWeek();
            States.CanPressForward = _iterator + 6 < _lastIndex;
            States.CanPressBack = _iterator - 1 > 0;
            for (var i = 0; i < 7; i++)
            {
                if (Week[i].IsThisDay(now.Year, now.Month, now.Day)) return (true, false, i);
            }
            return Week[0].IsFuture() ? (false, true, 0) : (false, false, -1);
        }

        private void MinusWeek()
        {
            if (_iterator - 7 <= 0)
            {
                MinusWeekIteratorNull();
            }
            else
            {
                MinusWeekIteratorNotNull();
            }

            Header = SetHeader();
            SetShortDayInfo();
        }

        private void MinusWeekIteratorNull()
        {
            for (var i = 6; i >= 0; i--)
            {
                _iterator--;
                Week[i] = _iterator >= 0 ? _days![_iterator] : new Day("past");
            }
        }

        private void MinusWeekIteratorNotNull()
        {
            for (var i = 6; i >= 0; i--)
            {
                _iterator--;
                Week[i] = _days![_iterator];
            }
        }

        public void CurrentWeekFunction()
        {
            if (States.StepsFromCurrentWeek > 0)
            {
                while (States.StepsFromCurrentWeek != 0)
                {
                    PreviousWeek();
                }
            }
            else
            {
                while (States.StepsFromCurrentWeek != 0)
                {
                    NextWeek();
                }
            }
        }

        private bool IsOverlayWithoutMessage(int index, int startTimeIndex, int endTimeIndex)
        {
            var isOverlay = false;
            if (_days![index].Lessons!.Count <= 0) return isOverlay;
            foreach (var lesson in _days![index].Lessons!)
            {

                if (startTimeIndex >= lesson.PositionInDayStart && startTimeIndex < lesson.PositionInDayStart + lesson.PositionInDayEnd)
                {
                    isOverlay = true;
                    break;
                }

                if (startTimeIndex < lesson.PositionInDayStart && endTimeIndex > lesson.PositionInDayStart)
                {
                    isOverlay = true;
                    break;
                }
            }

            return isOverlay;
        }
        private (bool isOverlay, string whatDay) IsOverlayWithMessage(int index, int startTimeIndex, int endTimeIndex)
        {
            var isOverlay = false;
            var whatDay = string.Empty;
            if (_days![index].Lessons!.Count <= 0) return (isOverlay, whatDay);
            foreach (var lesson in _days![index].Lessons!)
            {

                if (startTimeIndex >= lesson.PositionInDayStart && startTimeIndex < lesson.PositionInDayStart + lesson.PositionInDayEnd)
                {
                    isOverlay = true;
                    whatDay = $"\n{_days![index].Year} {_days![index].MonthToString()} {_days![index].Date}";
                    break;
                }

                if (startTimeIndex < lesson.PositionInDayStart && endTimeIndex > lesson.PositionInDayStart)
                {
                    isOverlay = true;
                    whatDay = $"\n{_days![index].Year} {_days![index].MonthToString()} {_days![index].Date}";
                    break;
                }
            }

            return (isOverlay, whatDay);
        }

        private (bool isOverlay, string whatDay) IsOverlayWhileAdding(Lesson lesson, int start, int stop)
        {
            for (var i = start; i <= stop; i += 7)
            {
                var (isOverlay, whatDay) = IsOverlayWithMessage(i, lesson.PositionInDayStart, lesson.PositionInDayStart + lesson.PositionInDayEnd);
                if (isOverlay)
                {
                    return (true, whatDay);
                }
            }
            return (false, string.Empty);
        }

        private (bool isOverlay, string whatDay) IsOverlayWhileCopying(Lesson lesson, int copyIndex, int start, int stop)
        {
            for (var i = start; i <= stop; i++)
            {
                if (_days![i].GetDayIndex() != copyIndex) continue;
                for (var j = i; j <= stop; j += 7)
                {
                    var (isOverlay, whatDay) = IsOverlayWithMessage(j, lesson.PositionInDayStart, lesson.PositionInDayStart + lesson.PositionInDayEnd);
                    if (isOverlay)
                    {
                        return (true, whatDay);
                    }
                }
                break;
            }
            return (false, string.Empty);
        }

        private bool IsOverlayTheSameDay(int dayIndex, int lessonIndex, int startTimeIndex, int endTimeIndex)
        {
            var isOverlay = false;

            for (var i = 0; i < _days![dayIndex].Lessons!.Count; i++)
            {
                if (i == lessonIndex) continue;

                if (startTimeIndex >= _days![dayIndex].Lessons![i].PositionInDayStart && startTimeIndex < _days![dayIndex].Lessons![i].PositionInDayStart + _days![dayIndex].Lessons![i].PositionInDayEnd)
                {
                    isOverlay = true;
                    break;
                }

                if (startTimeIndex < _days![dayIndex].Lessons![i].PositionInDayStart && endTimeIndex > _days![dayIndex].Lessons![i].PositionInDayStart)
                {
                    isOverlay = true;
                    break;
                }
            }

            return isOverlay;
        }

        public Lesson CreateLesson(string subject, string teacher, string auditorium, int startTimeIndex,
            int endTimeIndex, string duration, DateTime date)
        {
            var lesson = new Lesson(subject, teacher, auditorium, duration);
            lesson.SetDate(date);
            lesson.SetPositionInDayStart(startTimeIndex);
            lesson.SetPositionInDayEnd(endTimeIndex);
            return lesson;
        }

        public bool EditLesson(int dayIndex, int lessonIndex, string subject, string teacher, string auditorium,
            int startTimeIndex, int endTimeIndex, string duration, DateTime date, int year, int month, int day)
        {
            var newIndex = FindDay(year, month, day);
            var lesson = CreateLesson(subject, teacher, auditorium, startTimeIndex, endTimeIndex, duration, date);

            if (newIndex == dayIndex)
            {
                if (IsOverlayTheSameDay(dayIndex, lessonIndex, startTimeIndex, endTimeIndex)) return false;
                _days![dayIndex].Lessons![lessonIndex].Edit(lesson);
            }
            else
            {
                if (IsOverlayWithoutMessage(newIndex, startTimeIndex, endTimeIndex)) return false;
                _days![dayIndex].Lessons!.RemoveAt(lessonIndex);
                lesson.SetPositionInWeek(_days![newIndex].GetDayIndex());
                _days![newIndex].Lessons!.Add(lesson);
                var lesIndex = _days[newIndex].Lessons!.IndexOf(lesson);
                lesson.SetConnectionIndexes(newIndex, lesIndex);
            }

            Serializer.Save(_days);

            return true;
        }

        private void AddLesson(Lesson lesson, int start, int stop)
        {
            for (var i = start; i <= stop; i += 7)
            {
                var lessonToAdd = lesson.GetCopy();
                lessonToAdd.SetDate(_days![i].GetDateTime());
                _days[i].Lessons!.Add(lessonToAdd);
                var lesIndex = _days[i].Lessons!.IndexOf(lessonToAdd);
                _days[i].Lessons![lesIndex].SetConnectionIndexes(i, lesIndex);
            }
        }

        private void CopyLesson(Lesson lesson, int copyIndex, int start, int stop)
        {
            lesson.SetPositionInWeek(copyIndex);
            for (var i = start; i <= stop; i++)
            {
                if (_days![i].GetDayIndex() != copyIndex) continue;
                for (var j = i; j <= stop; j += 7)
                {
                    var lessonToAdd = lesson.GetCopy();
                    lessonToAdd.SetDate(_days[j].GetDateTime());
                    _days[j].Lessons!.Add(lessonToAdd);
                    var lesIndex = _days[j].Lessons!.IndexOf(lessonToAdd);
                    _days[j].Lessons![lesIndex].SetConnectionIndexes(j, lesIndex);
                }
                break;
            }
        }

        public (bool isDone, string whatDay) AddLessonToDays(Lesson lesson, int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo,
            int dayTo, int copy1Index, int copy2Index, int copy3Index)
        {
            var start = FindDay(yearFrom, monthFrom, dayFrom);
            var stop = FindDay(yearTo, monthTo, dayTo);
            lesson.SetPositionInWeek(_days![start].GetDayIndex());

            var check0 = IsOverlayWhileAdding(lesson, start, stop); if (check0.isOverlay) return (false, check0.whatDay);
            var check1 = IsOverlayWhileCopying(lesson, copy1Index, start, stop); if (copy1Index != -1 && check1.isOverlay) return (false, check1.whatDay);
            var check2 = IsOverlayWhileCopying(lesson, copy2Index, start, stop); if (copy2Index != -1 && check2.isOverlay) return (false, check2.whatDay);
            var check3 = IsOverlayWhileCopying(lesson, copy3Index, start, stop); if (copy3Index != -1 && check3.isOverlay) return (false, check3.whatDay);

            AddLesson(lesson, start, stop);
            if (copy1Index != -1)
            {
                CopyLesson(lesson, copy1Index, start, stop);
            }

            if (copy2Index != -1)
            {
                CopyLesson(lesson, copy2Index, start, stop);
            }

            if (copy3Index != -1)
            {
                CopyLesson(lesson, copy3Index, start, stop);
            }

            Serializer.Save(_days);
            return (true, string.Empty);
        }

        public bool AddLessonToOneDay(Lesson lesson, int yearFrom, int monthFrom, int dayFrom)
        {

            var dayIndex = FindDay(yearFrom, monthFrom, dayFrom);
            if (IsOverlayWithoutMessage(dayIndex, lesson.PositionInDayStart, lesson.PositionInDayStart + lesson.PositionInDayEnd))
            {
                return false;
            }
            lesson.SetPositionInWeek(_days![dayIndex].GetDayIndex());
            lesson.SetDate(_days![dayIndex].GetDateTime());
            _days![dayIndex].Lessons!.Add(lesson);
            var lesIndex = _days![dayIndex].Lessons!.IndexOf(lesson);
            _days![dayIndex].Lessons![lesIndex].SetConnectionIndexes(dayIndex, lesIndex);
            Serializer.Save(_days);
            return true;
        }

        public void DeleteSelectedLesson(int dayIndex, int lessonIndex)
        {
            var lesson = _days![dayIndex].Lessons![lessonIndex];
            var dayOfTheWeek = _days![dayIndex].GetDayIndex();
            Week[dayOfTheWeek].Lessons!.Remove(lesson);
            Week[dayOfTheWeek].RefreshConnectionLessonIndexes();
            _days[dayIndex].Lessons!.Remove(lesson);
            _days[dayIndex].RefreshConnectionLessonIndexes();
            Serializer.Save(_days);
        }

        public Lesson GetSelectedLesson(int dayIndex, int lessonIndex)
        {
            return _days![dayIndex].Lessons![lessonIndex];
        }
    }
}
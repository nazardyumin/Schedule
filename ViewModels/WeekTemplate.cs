using Schedule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Schedule.ViewModels
{
    public class WeekTemplate : Notifier
    {
        private List<Day>? _days;
        private int _currentDayIndex;

        private Day? _monday;

        public Day Monday
        {
            get => _monday!;
            set => SetField(ref _monday, value);
        }

        private Day? _tuesday;

        public Day Tuesday
        {
            get => _tuesday!;
            set => SetField(ref _tuesday, value);
        }

        private Day? _wednesday;

        public Day Wednesday
        {
            get => _wednesday!;
            set => SetField(ref _wednesday, value);
        }

        private Day? _thursday;

        public Day Thursday
        {
            get => _thursday!;
            set => SetField(ref _thursday, value);
        }

        private Day? _friday;

        public Day Friday
        {
            get => _friday!;
            set => SetField(ref _friday, value);
        }

        private Day? _saturday;

        public Day Saturday
        {
            get => _saturday!;
            set => SetField(ref _saturday, value);
        }

        private Day? _sunday;

        public Day Sunday
        {
            get => _sunday!;
            set => SetField(ref _sunday, value);
        }

        private string? _header;

        public string Header
        {
            get => _header!;
            set => SetField(ref _header, value);
        }

        private bool _canPressForward;

        public bool CanPressForward
        {
            get => _canPressForward;
            set => SetField(ref _canPressForward, value);
        }

        private bool _canPressBack;

        public bool CanPressBack
        {
            get => _canPressBack;
            set => SetField(ref _canPressBack, value);
        }

        private readonly int _lastIndex;
        private int _iterator;

        private bool _canPressCurrentWeek;

        public bool CanPressCurrentWeek
        {
            get => _canPressCurrentWeek;
            set => SetField(ref _canPressCurrentWeek, value);
        }

        private int _stepsFromCurrentWeek;

        private int StepsFromCurrentWeek
        {
            get => _stepsFromCurrentWeek;
            set
            {
                SetField(ref _stepsFromCurrentWeek, value);
                RefreshCanPressCurrentWeekState();
            }
        }

        public WeekTemplate()
        {
            SetDays();
            FocusOnCurrentWeek();
            CanPressForward = true;
            CanPressBack = true;
            _lastIndex = _days!.Count - 1;
            _iterator = _days.IndexOf(Monday);
            StepsFromCurrentWeek = 0;
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
                switch (currentDayOfTheWeek)
                {
                    case 0:
                        TodayIsMonday(index);
                        break;
                    case 1:
                        TodayIsTuesday(index);
                        break;
                    case 2:
                        TodayIsWednesday(index);
                        break;
                    case 3:
                        TodayIsThursday(index);
                        break;
                    case 4:
                        TodayIsFriday(index);
                        break;
                    case 5:
                        TodayIsSaturday(index);
                        break;
                    case 6:
                        TodayIsSunday(index);
                        break;
                }
            }

            _currentDayIndex = currentDayOfTheWeek;
        }

        private void TodayIsMonday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = _days[index];
            Tuesday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Wednesday = index + 2 <= lastIndex ? _days[index + 2] : new Day("future");
            Thursday = index + 3 <= lastIndex ? _days[index + 3] : new Day("future");
            Friday = index + 4 <= lastIndex ? _days[index + 4] : new Day("future");
            Saturday = index + 5 <= lastIndex ? _days[index + 5] : new Day("future");
            Sunday = index + 6 <= lastIndex ? _days[index + 6] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsTuesday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = index - 1 >= 0 ? _days[index - 1] : new Day("past");
            Tuesday = _days[index];
            Wednesday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Thursday = index + 2 <= lastIndex ? _days[index + 2] : new Day("future");
            Friday = index + 3 <= lastIndex ? _days[index + 3] : new Day("future");
            Saturday = index + 4 <= lastIndex ? _days[index + 4] : new Day("future");
            Sunday = index + 5 <= lastIndex ? _days[index + 5] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsWednesday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = index - 2 >= 0 ? _days[index - 2] : new Day("past");
            Tuesday = index - 1 >= 0 ? _days[index - 1] : new Day("past");
            Wednesday = _days[index];
            Thursday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Friday = index + 2 <= lastIndex ? _days[index + 2] : new Day("future");
            Saturday = index + 3 <= lastIndex ? _days[index + 3] : new Day("future");
            Sunday = index + 4 <= lastIndex ? _days[index + 4] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsThursday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = index - 3 >= 0 ? _days[index - 3] : new Day("past");
            Tuesday = index - 2 >= 0 ? _days[index - 2] : new Day("past");
            Wednesday = index - 1 >= 0 ? _days[index - 1] : new Day("past");
            Thursday = _days[index];
            Friday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Saturday = index + 2 <= lastIndex ? _days[index + 2] : new Day("future");
            Sunday = index + 3 <= lastIndex ? _days[index + 3] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsFriday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = index - 4 >= 0 ? _days[index - 4] : new Day("past");
            Tuesday = index - 3 >= 0 ? _days[index - 3] : new Day("past");
            Wednesday = index - 2 >= 0 ? _days[index - 2] : new Day("past");
            Thursday = index - 1 >= 0 ? _days[index - 1] : new Day("past");
            Friday = _days[index];
            Saturday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Sunday = index + 2 <= lastIndex ? _days[index + 2] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsSaturday(int index)
        {
            var lastIndex = _days!.Count - 1;
            Monday = index - 5 >= 0 ? _days[index - 5] : new Day("past");
            Tuesday = index - 4 >= 0 ? _days[index - 4] : new Day("past");
            Wednesday = index - 3 >= 0 ? _days[index - 3] : new Day("past");
            Thursday = index - 2 >= 0 ? _days[index - 2] : new Day("past");
            Friday = index - 1 >= 0 ? _days[index - 1] : new Day("past");
            Saturday = _days[index];
            Sunday = index + 1 <= lastIndex ? _days[index + 1] : new Day("future");
            Header = SetHeader();
        }

        private void TodayIsSunday(int index)
        {
            Monday = index - 6 >= 0 ? _days![index - 6] : new Day("past");
            Tuesday = index - 5 >= 0 ? _days![index - 5] : new Day("past");
            Wednesday = index - 4 >= 0 ? _days![index - 4] : new Day("past");
            Thursday = index - 3 >= 0 ? _days![index - 3] : new Day("past");
            Friday = index - 2 >= 0 ? _days![index - 2] : new Day("past");
            Saturday = index - 1 >= 0 ? _days![index - 1] : new Day("past");
            Sunday = _days![index];
            Header = SetHeader();
        }

        private string SetHeader()
        {
            return $"{Monday.GetDayInfo()} - {Sunday.GetDayInfo()}";
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
            StepsFromCurrentWeek++;
            var now = DateTime.Now;
            PlusWeek();
            CanPressForward = _iterator + 6 < _lastIndex;
            CanPressBack = _iterator - 1 > 0;
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            return Monday.IsFuture() ? (false, true, 0) : (false, false, -1);
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
        }

        private void PlusWeekIteratorNotNull()
        {
            Monday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Tuesday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Wednesday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Thursday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Friday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Saturday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
            Sunday = _iterator + 7 <= _lastIndex ? _days![_iterator + 7] : new Day("future");
            _iterator++;
        }

        private void PlusWeekIteratorNull()
        {
            Monday = _days![_iterator + 7];
            _iterator++;
            Tuesday = _days![_iterator + 7];
            _iterator++;
            Wednesday = _days![_iterator + 7];
            _iterator++;
            Thursday = _days![_iterator + 7];
            _iterator++;
            Friday = _days![_iterator + 7];
            _iterator++;
            Saturday = _days![_iterator + 7];
            _iterator++;
            Sunday = _days![_iterator + 7];
            _iterator++;
        }

        public (bool isCurrentWeek, bool isFuture, int index) PreviousWeek()
        {
            StepsFromCurrentWeek--;
            var now = DateTime.Now;
            MinusWeek();
            CanPressForward = _iterator + 6 < _lastIndex;
            CanPressBack = _iterator - 1 > 0;
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            return Monday.IsFuture() ? (false, true, 0) : (false, false, -1);
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
        }

        private void MinusWeekIteratorNull()
        {
            _iterator--;
            Sunday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Saturday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Friday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Thursday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Wednesday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Tuesday = _iterator >= 0 ? _days![_iterator] : new Day("past");
            _iterator--;
            Monday = _iterator >= 0 ? _days![_iterator] : new Day("past");
        }

        private void MinusWeekIteratorNotNull()
        {
            _iterator--;
            Sunday = _days![_iterator];
            _iterator--;
            Saturday = _days![_iterator];
            _iterator--;
            Friday = _days![_iterator];
            _iterator--;
            Thursday = _days![_iterator];
            _iterator--;
            Wednesday = _days![_iterator];
            _iterator--;
            Tuesday = _days![_iterator];
            _iterator--;
            Monday = _days![_iterator];
        }

        private void RefreshCanPressCurrentWeekState()
        {
            CanPressCurrentWeek = StepsFromCurrentWeek is >= 2 or <= -2;
        }

        public void CurrentWeekFunction()
        {
            if (StepsFromCurrentWeek > 0)
            {
                while (StepsFromCurrentWeek != 0)
                {
                    PreviousWeek();
                }
            }
            else
            {
                while (StepsFromCurrentWeek != 0)
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
        private (bool isOverlay, string whatDay) IsOverlay(int index, int startTimeIndex, int endTimeIndex)
        {
            var isOverlay = false;
            var whatDay=string.Empty;
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

        private (bool isOverlay,string whatDay) IsOverlayWhileAdding(Lesson lesson, int start, int stop)
        {
            for (var i = start; i <= stop; i += 7)
            {
                var (isOverlay, whatDay) = IsOverlay(i, lesson.PositionInDayStart, lesson.PositionInDayStart + lesson.PositionInDayEnd);
                if (isOverlay)
                {
                    return (true,whatDay);
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
                    var (isOverlay, whatDay) = IsOverlay(j, lesson.PositionInDayStart, lesson.PositionInDayStart + lesson.PositionInDayEnd);
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
            RemoveLessonFromCurrentWeek(dayOfTheWeek, lesson);
            RefreshConnectionLessonIndexesInView(dayOfTheWeek);
            _days[dayIndex].Lessons!.Remove(lesson);
            RefreshConnectionLessonIndexes(dayIndex);
            Serializer.Save(_days);
        }

        private void RefreshConnectionLessonIndexes(int index)
        {
            foreach (var lesson in _days![index].Lessons!)
            {
                lesson.ConnectionLessonIndex = _days![index].Lessons!.IndexOf(lesson);
            }
        }

        private void RefreshConnectionLessonIndexesInView(int dayOfTheWeek)
        {
            switch (dayOfTheWeek)
            {
                case 0:
                    foreach (var lesson in Monday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Monday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 1:
                    foreach (var lesson in Tuesday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Tuesday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 2:
                    foreach (var lesson in Wednesday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Wednesday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 3:
                    foreach (var lesson in Thursday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Thursday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 4:
                    foreach (var lesson in Friday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Friday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 5:
                    foreach (var lesson in Saturday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Saturday.Lessons!.IndexOf(lesson);
                    }
                    break;
                case 6:
                    foreach (var lesson in Sunday.Lessons!)
                    {
                        lesson.ConnectionLessonIndex = Sunday.Lessons!.IndexOf(lesson);
                    }
                    break;
            }
        }

        private void RemoveLessonFromCurrentWeek(int dayOfTheWeek, Lesson lesson)
        {
            switch (dayOfTheWeek)
            {
                case 0:
                    Monday.Lessons!.Remove(lesson);
                    break;
                case 1:
                    Tuesday.Lessons!.Remove(lesson);
                    break;
                case 2:
                    Wednesday.Lessons!.Remove(lesson);
                    break;
                case 3:
                    Thursday.Lessons!.Remove(lesson);
                    break;
                case 4:
                    Friday.Lessons!.Remove(lesson);
                    break;
                case 5:
                    Saturday.Lessons!.Remove(lesson);
                    break;
                case 6:
                    Sunday.Lessons!.Remove(lesson);
                    break;
            }
        }

        public Lesson GetSelectedLesson(int dayIndex, int lessonIndex)
        {
            return _days![dayIndex].Lessons![lessonIndex];
        }
    }
}
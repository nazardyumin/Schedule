﻿using Schedule.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.ViewModels
{
    public class WeekTemplate : Notifier
    {
        private List<Day>? Days;
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
        public List<int>? ComboBoxYears { get; set; }
        public List<string>? ComboBoxMonths { get; set; }
        public List<int>? ComboBoxDates { get; set; }
        public List<string>? ComboBoxTime { get; set; }
        public List<string>? ComboBoxDays { get; set; }

        public WeekTemplate()
        {
            SetDays();
            FocuseOnCurrentWeek();
        }

        private void SetDays()
        {
            Days = Serializer.Load();
            var years = Configurator.Load().Years!;
            foreach (var year in years)
            {
                if (year > Days[^1].Year)
                {
                    var firstDay = new DateTime(year, 1, 1);
                    var lastDay = new DateTime(years[^1], 12, 31);
                    var result = lastDay - firstDay;
                    for (int i = 0; i <= result.TotalDays; i++)
                    {
                        var timeSpan = new TimeSpan(i, 0, 0, 0);
                        Days.Add(new Day(firstDay + timeSpan));
                    }
                    Serializer.Save(Days);
                    break;
                }
            }
        }
        public void FocuseOnCurrentWeek()
        {
            DateTime currentDate = DateTime.Now;
            int index;
            int currentDayOfTheWeek = 0;
            foreach (var item in from item in Days!
                                 where item.IsThisDay(currentDate.Year, currentDate.Month, currentDate.Day)
                                 select item)
            {
                index = Days!.IndexOf(item);
                currentDayOfTheWeek = Days[index].GetDayIndex();
                var lastIndex = Days.Count - 1;
                switch (currentDayOfTheWeek)
                {
                    case 0:
                        Monday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Tuesday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Wednesday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Thursday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Friday = Days[index + 4];
                        }
                        if (index + 5 <= lastIndex)
                        {
                            Saturday = Days[index + 5];
                        }
                        if (index + 6 <= lastIndex)
                        {
                            Sunday = Days[index + 6];
                        }
                        Header = SetHeader();
                        break;
                    case 1:
                        if (index - 1 >= 0)
                        {
                            Monday = Days[index - 1];
                        }
                        Tuesday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Wednesday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Thursday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Friday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Saturday = Days[index + 4];
                        }
                        if (index + 5 <= lastIndex)
                        {
                            Sunday = Days[index + 5];
                        }
                        Header = SetHeader();
                        break;
                    case 2:
                        if (index - 2 >= 0)
                        {
                            Monday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Tuesday = Days[index - 1];
                        }
                        Wednesday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Thursday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Friday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Saturday = Days[index + 3];
                        }
                        if (index + 4 <= lastIndex)
                        {
                            Sunday = Days[index + 4];
                        }
                        Header = SetHeader();
                        break;
                    case 3:
                        if (index - 3 >= 0)
                        {
                            Monday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Tuesday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Wednesday = Days[index - 1];
                        }
                        Thursday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Friday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Saturday = Days[index + 2];
                        }
                        if (index + 3 <= lastIndex)
                        {
                            Sunday = Days[index + 3];
                        }
                        Header = SetHeader();
                        break;
                    case 4:
                        if (index - 4 >= 0)
                        {
                            Monday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Tuesday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Wednesday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Thursday = Days[index - 1];
                        }
                        Friday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Saturday = Days[index + 1];
                        }
                        if (index + 2 <= lastIndex)
                        {
                            Sunday = Days[index + 2];
                        }
                        Header = SetHeader();
                        break;
                    case 5:
                        if (index - 5 >= 0)
                        {
                            Monday = Days[index - 5];
                        }
                        if (index - 4 >= 0)
                        {
                            Tuesday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Wednesday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Thursday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Friday = Days[index - 1];
                        }
                        Saturday = Days[index];
                        if (index + 1 <= lastIndex)
                        {
                            Sunday = Days[index + 1];
                        }
                        Header = SetHeader();
                        break;
                    case 6:
                        if (index - 6 >= 0)
                        {
                            Monday = Days[index - 6];
                        }
                        if (index - 5 >= 0)
                        {
                            Tuesday = Days[index - 5];
                        }
                        if (index - 4 >= 0)
                        {
                            Wednesday = Days[index - 4];
                        }
                        if (index - 3 >= 0)
                        {
                            Thursday = Days[index - 3];
                        }
                        if (index - 2 >= 0)
                        {
                            Friday = Days[index - 2];
                        }
                        if (index - 1 >= 0)
                        {
                            Saturday = Days[index - 1];
                        }
                        Sunday = Days[index];
                        Header = SetHeader();
                        break;
                }
            }
            _currentDayIndex = currentDayOfTheWeek;
        }
        private string SetHeader()
        {
            return $"{Monday.GetDayInfo()} - {Sunday.GetDayInfo()}";
        }
        public int GetCurrentDayIndex()
        {
            return _currentDayIndex;
        }

        public (bool isCurrentWeek, bool isFuture, int index) NextWeek()
        {
            DateTime now = DateTime.Now;
            Monday = Days![Days.IndexOf(Monday) + 7];
            Tuesday = Days![Days.IndexOf(Tuesday) + 7];
            Wednesday = Days![Days.IndexOf(Wednesday) + 7];
            Thursday = Days![Days.IndexOf(Thursday) + 7];
            Friday = Days![Days.IndexOf(Friday) + 7];
            Saturday = Days![Days.IndexOf(Saturday) + 7];
            Sunday = Days![Days.IndexOf(Sunday) + 7];
            Header = SetHeader();
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            if (Monday.IsFuture()) return (false, true, 0);
            if (Tuesday.IsFuture()) return (false, true, 1);
            if (Wednesday.IsFuture()) return (false, true, 2);
            if (Thursday.IsFuture()) return (false, true, 3);
            if (Friday.IsFuture()) return (false, true, 4);
            if (Saturday.IsFuture()) return (false, true, 5);
            if (Sunday.IsFuture()) return (false, true, 6);
            return (false, false, -1);
        }
        public (bool isCurrentWeek, bool isFuture, int index) PreviousWeek()
        {
            DateTime now = DateTime.Now;
            Monday = Days![Days.IndexOf(Monday) - 7];
            Tuesday = Days![Days.IndexOf(Tuesday) - 7];
            Wednesday = Days![Days.IndexOf(Wednesday) - 7];
            Thursday = Days![Days.IndexOf(Thursday) - 7];
            Friday = Days![Days.IndexOf(Friday) - 7];
            Saturday = Days![Days.IndexOf(Saturday) - 7];
            Sunday = Days![Days.IndexOf(Sunday) - 7];
            Header = SetHeader();
            if (Monday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 0);
            if (Tuesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 1);
            if (Wednesday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 2);
            if (Thursday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 3);
            if (Friday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 4);
            if (Saturday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 5);
            if (Sunday.IsThisDay(now.Year, now.Month, now.Day)) return (true, false, 6);
            if (Monday.IsFuture()) return (false, true, 0);
            if (Tuesday.IsFuture()) return (false, true, 1);
            if (Wednesday.IsFuture()) return (false, true, 2);
            if (Thursday.IsFuture()) return (false, true, 3);
            if (Friday.IsFuture()) return (false, true, 4);
            if (Saturday.IsFuture()) return (false, true, 5);
            if (Sunday.IsFuture()) return (false, true, 6);
            return (false, false, -1);
        }

        public List<Day> GetDays()
        {
            return Days!;
        }
        public Lesson CreateLesson(string subject, string teacher, string auditorium, int startTimeIndex, int endTimeIndex, string duration, DateTime date)
        {
            var lesson = new Lesson(subject, teacher, auditorium, duration);
            lesson.SetDate(date);
            lesson.SetPositionInDayStart(startTimeIndex);
            lesson.SetPositionInDayEnd(endTimeIndex);
            return lesson;
        }
        public void EditLesson(int dayIndex, int lessonIndex, string subject, string teacher, string auditorium, int startTimeIndex, int endTimeIndex, string duration, DateTime date, int year, int month, int day)
        {
            int newIndex = FindDay(year, month, day);
            var lesson = new Lesson(subject, teacher, auditorium, duration);
            lesson.SetDate(date);
            lesson.SetPositionInDayStart(startTimeIndex);
            lesson.SetPositionInDayEnd(endTimeIndex);

            if (newIndex == dayIndex)
            {
                Days![dayIndex].Lessons![lessonIndex].Edit(lesson);
            }
            else
            {
                Days![dayIndex].Lessons!.RemoveAt(lessonIndex);
                lesson.SetPositionInWeek(Days![newIndex].GetDayIndex());
                Days![newIndex].Lessons!.Add(lesson);
                var lesIndex = Days[newIndex].Lessons!.IndexOf(lesson);
                lesson.SetConnectionIndexes(newIndex, lesIndex);
            }
            Serializer.Save(Days);
        }
        public void AddLessonToDays(Lesson lesson, int yearFrom, int monthFrom, int dayFrom, int yearTo, int monthTo, int dayTo, int copy1Index, int copy2Index, int copy3Index)
        {
            int start = FindDay(yearFrom, monthFrom, dayFrom);
            int stop = FindDay(yearTo, monthTo, dayTo);
            lesson.SetPositionInWeek(Days![start].GetDayIndex());
            for (int i = start; i <= stop; i += 7)
            {
                var lessonToAdd = lesson.GetCopy();
                lessonToAdd.SetDate(Days[i].GetDateTime());
                Days[i].Lessons!.Add(lessonToAdd);
                var lesIndex = Days[i].Lessons!.IndexOf(lessonToAdd);
                Days[i].Lessons![lesIndex].SetConnectionIndexes(i, lesIndex);
            }
            if (copy1Index != -1)
            {
                lesson.SetPositionInWeek(copy1Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy1Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            var lessonToAdd = lesson.GetCopy();
                            lessonToAdd.SetDate(Days[j].GetDateTime());
                            Days[j].Lessons!.Add(lessonToAdd);
                            var lesIndex = Days[j].Lessons!.IndexOf(lessonToAdd);
                            Days[j].Lessons![lesIndex].SetConnectionIndexes(j, lesIndex);
                        }
                        break;
                    }
                }
            }
            if (copy2Index != -1)
            {
                lesson.SetPositionInWeek(copy2Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy2Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            var lessonToAdd = lesson.GetCopy();
                            lessonToAdd.SetDate(Days[j].GetDateTime());
                            Days[j].Lessons!.Add(lessonToAdd);
                            var lesIndex = Days[j].Lessons!.IndexOf(lessonToAdd);
                            Days[j].Lessons![lesIndex].SetConnectionIndexes(j, lesIndex);
                        }
                        break;
                    }
                }
            }
            if (copy3Index != -1)
            {
                lesson.SetPositionInWeek(copy3Index);
                for (int i = start; i <= stop; i++)
                {
                    if (Days[i].GetDayIndex() == copy3Index)
                    {
                        for (int j = i; j <= stop; j += 7)
                        {
                            var lessonToAdd = lesson.GetCopy();
                            lessonToAdd.SetDate(Days[j].GetDateTime());
                            Days[j].Lessons!.Add(lessonToAdd);
                            var lesIndex = Days[j].Lessons!.IndexOf(lessonToAdd);
                            Days[j].Lessons![lesIndex].SetConnectionIndexes(j, lesIndex);
                        }
                        break;
                    }
                }
            }
            Serializer.Save(Days);
        }
        public void AddLessonToOneDay(Lesson lesson, int yearFrom, int monthFrom, int dayFrom)
        {
            var dayIndex = FindDay(yearFrom, monthFrom, dayFrom);
            Days![dayIndex].Lessons!.Add(lesson);
            var lesIndex = Days![dayIndex].Lessons!.IndexOf(lesson);
            Days![dayIndex].Lessons![lesIndex].SetConnectionIndexes(dayIndex, lesIndex);
            Serializer.Save(Days);
        }
        private int FindDay(int year, int month, int date)
        {
            int index = -1;
            for (int i = 0; i < Days!.Count; i++)
            {
                if (Days[i].IsThisDay(year, month, date))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public bool IsCurrentWeek()
        {
            DateTime now = DateTime.Now;
            return Monday.IsThisDay(now.Year, now.Month, now.Day) || Tuesday.IsThisDay(now.Year, now.Month, now.Day) || Wednesday.IsThisDay(now.Year, now.Month, now.Day) ||
                   Thursday.IsThisDay(now.Year, now.Month, now.Day) || Friday.IsThisDay(now.Year, now.Month, now.Day) || Saturday.IsThisDay(now.Year, now.Month, now.Day) ||
                   Sunday.IsThisDay(now.Year, now.Month, now.Day);
        }
        public void DeleteSelectedLesson(int dayIndex, int lessonIndex)
        {
            var lesson = Days![dayIndex].Lessons![lessonIndex];
            var dayOfTheWeek = Days![dayIndex].GetDayIndex();
            RemoveLessonFromCurrentWeek(dayOfTheWeek, lesson);
            Days[dayIndex].Lessons!.Remove(lesson);
            Serializer.Save(Days);
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
            return Days![dayIndex].Lessons![lessonIndex];
        }
    }
}

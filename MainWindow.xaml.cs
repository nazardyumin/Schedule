﻿using MaterialDesignThemes.Wpf;
using Schedule.Model;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Schedule
{
    public partial class MainWindow : Window
    {
        public WeekTemplate Model { get; set; }
        public CalendarTemplate AddingSection { get; set; }
        public string ButtonBackContent { get; set; }
        public string ButtonForwardContent { get; set; }
        //private Timer? _timer;
        private int monthFromMemory;
        private int monthToMemory;
        public MainWindow()
        {
            Model = new();
            AddingSection = new();
            ButtonBackContent = "<<";
            ButtonForwardContent =">>";
            monthFromMemory = 0;
            monthToMemory = 0;
            //SetTimer();
            InitializeComponent();
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
            TodayBackground(Model.GetCurrentDayIndex());


        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.PreviousWeek();
            if (result.isCurrentWeek) TodayBackground(result.index);
            else if (result.isFuture) FutureBackground(result.index);
            else PastBackground();
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.NextWeek();
            if (result.isCurrentWeek) TodayBackground(result.index);
            else if (result.isFuture) FutureBackground(result.index);
            else PastBackground();
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
        }

        private void ButtonToday_Click(object sender, RoutedEventArgs e)
        {
            var result = AddingSection.GetTodayDateAndDay();
            ComboBoxYearFrom.SelectedIndex = ComboBoxYearFrom.Items.IndexOf(result.year);
            AddingSection.SetMonthsFromDependOnCalendar(result.year);
            ComboBoxMonthFrom.SelectedIndex = ComboBoxMonthFrom.Items.IndexOf(AddingSection.MonthToString(result.month));
            AddingSection.SetDatesFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), monthFromMemory);
            ComboBoxDayFrom.SelectedIndex = ComboBoxDayFrom.Items.IndexOf(result.day);
            ButtonToday.Content = result.name;
            ButtonToday.IsEnabled = false;
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                 ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            InputSubject.Clear();
            InputTeacher.Clear();
            InputAuditorium.Clear();
            ComboBoxStartTime.SelectedIndex = -1;
            ComboBoxEndTime.SelectedIndex = -1;
            ComboBoxYearFrom.SelectedIndex = -1;
            ComboBoxMonthFrom.SelectedIndex = -1;
            ComboBoxDayFrom.SelectedIndex = -1;
            ComboBoxYearTo.SelectedIndex = -1;
            ComboBoxMonthTo.SelectedIndex = -1;
            ComboBoxDayTo.SelectedIndex = -1;
            ButtonToday.Content = "Today";
            ButtonTargetDay.Content = string.Empty;
            ButtonToday.IsEnabled = true;
            ComboBoxCopy1.SelectedIndex = -1;
            ComboBoxCopy2.SelectedIndex = -1;
            ComboBoxCopy2.Visibility = Visibility.Hidden;
            ComboBoxCopy3.Visibility = Visibility.Hidden;
            AddingSection.ClearMonthsAndDates();
            AddingSection.ClearCopyDays();
            monthFromMemory = 0;
            monthToMemory = 0;
            ButtonAdd.IsEnabled = false;
            ButtonClear.IsEnabled = false;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var lesson = Model.CreateLesson(InputSubject.Text, InputTeacher.Text, InputAuditorium.Text, ComboBoxStartTime.SelectedIndex, ComboBoxEndTime.SelectedIndex, $"{ComboBoxStartTime.SelectedValue} - {ComboBoxEndTime.SelectedValue}");
            if (ComboBoxYearTo.SelectedIndex==-1|| ComboBoxMonthTo.SelectedIndex==-1|| ComboBoxDayTo.SelectedIndex==-1)
            {
                Model.AddLessonToOneDay(lesson, (int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue);
            }
            else
            {
                Model.AddLessonToDays(lesson, (int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue,
                                                            (int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue,
                                                            ComboBoxCopy1.SelectedIndex, ComboBoxCopy2.SelectedIndex, ComboBoxCopy3.SelectedIndex);
            }                     
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
            InputSubject.Clear();
            InputTeacher.Clear();
            InputAuditorium.Clear();
            ComboBoxStartTime.SelectedIndex = -1;
            ComboBoxEndTime.SelectedIndex = -1;
            ComboBoxYearFrom.SelectedIndex = -1;
            ComboBoxMonthFrom.SelectedIndex = -1;
            ComboBoxDayFrom.SelectedIndex = -1;
            ComboBoxYearTo.SelectedIndex = -1;
            ComboBoxMonthTo.SelectedIndex = -1;
            ComboBoxDayTo.SelectedIndex = -1;
            ButtonToday.Content = "Today";
            ButtonTargetDay.Content = string.Empty;
            ButtonToday.IsEnabled = true;
            ComboBoxCopy1.SelectedIndex = -1;
            ComboBoxCopy2.SelectedIndex = -1;
            ComboBoxCopy2.Visibility = Visibility.Hidden;
            ComboBoxCopy3.Visibility = Visibility.Hidden;
            AddingSection.ClearMonthsAndDates();
            AddingSection.ClearCopyDays();
            monthFromMemory = 0;
            monthToMemory = 0;
            ButtonAdd.IsEnabled = false;
            ButtonClear.IsEnabled = false;
        }
        //private void SetTimer()
        //{
        //    var timeSpan = new TimeSpan(1, 0, 0, 0);
        //    DateTime temp = DateTime.Now + timeSpan;
        //    var tomorrow=new DateTime(temp.Year,temp.Month,temp.Day,0,0,0,0);
        //    DateTime now = DateTime.Now;
        //    var timeToNewDay = new TimeSpan(tomorrow.Ticks - now.Ticks);
        //    _timer = new Timer(timeToNewDay.TotalMilliseconds);
        //    _timer.Start();
        //    _timer.Elapsed += Timer_ChangeDay!;
        //}

        //private void Timer_ChangeDay(Object source, ElapsedEventArgs e)
        //{
        //    _timer!.Stop();
        //    if (Model.IsCurrentWeek())
        //    {
        //        Model.FocuseOnCurrentWeek();
        //        Model.AddAllCardsToMondayGrid(ref GridMonday);
        //        Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
        //        Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
        //        Model.AddAllCardsToThursdayGrid(ref GridThursday);
        //        Model.AddAllCardsToFridayGrid(ref GridFriday);
        //        Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
        //        Model.AddAllCardsToSundayGrid(ref GridSunday);
        //        TodayBackground(Model.GetCurrentDayIndex());
        //    }          
        //    SetTimer();
        //}
        private void InputSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputSubject.Text.Length>0)
            {
                ButtonClear.IsEnabled = true;
                var check = InputSubject.Text.ToCharArray();
                bool hasLetters = false;
                foreach (var item in check)
                {
                    hasLetters = char.IsLetter(item);
                }
                if (!hasLetters)
                {
                    ButtonAdd.IsEnabled = false;
                    return;
                }
                if (InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex>-1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex>-1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex==-1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                }                             
            }
            else
            {
                if (InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 || ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 || 
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex>-1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }                   
                ButtonAdd.IsEnabled = false;
            }
        }
        private void InputTeacher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputTeacher.Text.Length > 0)
            {
                ButtonClear.IsEnabled = true;
                var check = InputTeacher.Text.ToCharArray();
                bool hasLetters = false;
                foreach (var item in check)
                {
                    hasLetters = char.IsLetter(item);
                }
                if (!hasLetters)
                {
                    ButtonAdd.IsEnabled = false;
                    return;
                }
                if (InputSubject.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (InputSubject.Text.Length > 0 || InputAuditorium.Text.Length > 0 || ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
                ButtonAdd.IsEnabled = false;
            }
        }
        private void InputAuditorium_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputAuditorium.Text.Length > 0)
            {
                ButtonClear.IsEnabled = true;
                var check = InputAuditorium.Text.ToCharArray();
                bool hasLetters = false;
                foreach (var item in check)
                {
                    hasLetters = char.IsLetter(item);
                }
                if (!hasLetters)
                {
                    ButtonAdd.IsEnabled = false;
                    return;
                }
                if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
                ButtonAdd.IsEnabled = false;
            }
        }
        private void ComboBoxStartTime_DropDownClosed(object sender, EventArgs e)
        {
            AddingSection.SetEndTime(ComboBoxStartTime.SelectedIndex);
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 || 
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxEndTime_DropDownClosed(object sender, EventArgs e)
        {
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxYearFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex != -1)
            {
                AddingSection.SetMonthsFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue);
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxMonthFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex != -1 && ComboBoxMonthFrom.SelectedIndex != -1)
            {
                AddingSection.SetDatesFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), monthFromMemory);
                monthFromMemory = AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue);
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxDayFrom_DropDownClosed(object sender, EventArgs e)
        {
            ChangeButtonTodayContent();
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxYearTo_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearTo.SelectedIndex != -1)
            {
                AddingSection.SetMonthsToDependOnCalendar((int)ComboBoxYearTo.SelectedValue);
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxMonthTo_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearTo.SelectedIndex != -1 && ComboBoxMonthTo.SelectedIndex != -1)
            {
                AddingSection.SetDatesToDependOnCalendar((int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), monthToMemory);
                monthToMemory = AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue);
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxDayTo_DropDownClosed(object sender, EventArgs e)
        {
            ChangeButtonTargetDayContent();
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxCopy1_DropDownClosed(object sender, EventArgs e)
        {
            
            if (ComboBoxCopy1.SelectedIndex > -1)
            {
                AddingSection.SetCopyDays2(ComboBoxCopy1.SelectedIndex);
                ComboBoxCopy2.Visibility = Visibility.Visible;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxCopy2_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxCopy2.SelectedIndex > -1)
            {
                AddingSection.SetCopyDays3(ComboBoxCopy2.SelectedIndex);
                ComboBoxCopy3.Visibility = Visibility.Visible;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ComboBoxCopy3_DropDownClosed(object sender, EventArgs e)
        {
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                if (InputSubject.Text.Length > 0 || InputTeacher.Text.Length > 0 || InputAuditorium.Text.Length > 0 ||
                    ComboBoxStartTime.SelectedIndex > -1 || ComboBoxEndTime.SelectedIndex > -1 ||
                    ComboBoxYearFrom.SelectedIndex > -1 || ComboBoxMonthFrom.SelectedIndex > -1 || ComboBoxDayFrom.SelectedIndex > -1 ||
                    ComboBoxYearTo.SelectedIndex > -1 || ComboBoxMonthTo.SelectedIndex > -1 || ComboBoxDayTo.SelectedIndex > -1 ||
                    ComboBoxCopy1.SelectedIndex > -1 || ComboBoxCopy2.SelectedIndex > -1 || ComboBoxCopy3.SelectedIndex > -1)
                {
                    ButtonClear.IsEnabled = true;
                }
                else
                {
                    ButtonClear.IsEnabled = false;
                }
            }
        }
        private void ChangeButtonTodayContent()
        {
            if (ComboBoxYearFrom.SelectedIndex != -1 && ComboBoxMonthFrom.SelectedIndex != -1 && ComboBoxDayFrom.SelectedIndex != -1)
            {
                ButtonToday.Content = AddingSection.GetSelectedDay((int)ComboBoxYearFrom.SelectedValue,
                                                                   AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue),
                                                                   (int)ComboBoxDayFrom.SelectedValue);
                ButtonToday.IsEnabled = false;
            }
        }
        private void ChangeButtonTargetDayContent()
        {
            if (ComboBoxYearTo.SelectedIndex != -1 && ComboBoxMonthTo.SelectedIndex != -1 && ComboBoxDayTo.SelectedIndex != -1)
            {
                ButtonTargetDay.Content = AddingSection.GetSelectedDay((int)ComboBoxYearTo.SelectedValue,
                                                                       AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue),
                                                                       (int)ComboBoxDayTo.SelectedValue);
            }
        }
        private void TodayBackground(int column)
        {
            switch (column)
            {
                case 0:
                    Border0.Background = new SolidColorBrush(Colors.LightYellow);
                    Border1.Background = new SolidColorBrush(Colors.White);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.LightYellow);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 2:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.LightYellow);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 3:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.LightYellow);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 4:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border4.Background = new SolidColorBrush(Colors.LightYellow);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 5:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border4.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border5.Background = new SolidColorBrush(Colors.LightYellow);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 6:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border4.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border5.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border6.Background = new SolidColorBrush(Colors.LightYellow);
                    break;
                default:
                    break;
            }
        }
        private void FutureBackground(int column)
        {
            switch (column)
            {
                case 0:
                    Border0.Background = new SolidColorBrush(Colors.White);
                    Border1.Background = new SolidColorBrush(Colors.White);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    Border0.Background = new SolidColorBrush(Colors.LightYellow);
                    Border1.Background = new SolidColorBrush(Colors.White);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 2:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.LightYellow);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 3:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.LightYellow);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 4:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.LightYellow);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 5:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border4.Background = new SolidColorBrush(Colors.LightYellow);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 6:
                    Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border4.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    Border5.Background = new SolidColorBrush(Colors.LightYellow);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                default:
                    break;
            }
        }
        private void PastBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border4.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border5.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border6.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }
    }
}

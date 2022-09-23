using MaterialDesignThemes.Wpf;
using Schedule.Model;
using Schedule.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Schedule
{
    public partial class MainWindow : Window
    {
        public WeekTemplate Model { get; set; }
        public AddingSectionTemplate AddingSection { get; set; }
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
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
            TodayBackground(Model.GetCurrentDayIndex());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.PreviousWeek();
            if (result.isCurrentWeek) TodayBackground(result.index);
            else if (result.isFuture) FutureBackground();
            else PastBackground();
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.NextWeek();
            if (result.isCurrentWeek) TodayBackground(result.index);
            else if (result.isFuture) FutureBackground();
            else PastBackground();
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
        }

        private void ButtonToday_Click(object sender, RoutedEventArgs e)
        {
            var result = AddingSection.GetTodayDateAndDay();
            ComboBoxYearFrom.SelectedIndex = ComboBoxYearFrom.Items.IndexOf(result.year);
            AddingSection.SetMonthsFromDependOnCalendar(result.year);
            ComboBoxMonthFrom.SelectedIndex = ComboBoxMonthFrom.Items.IndexOf(AddingSection.MonthToString(result.month));
            AddingSection.SetDatesFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), monthFromMemory);
            monthFromMemory = AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue);
            ComboBoxDayFrom.SelectedIndex = ComboBoxDayFrom.Items.IndexOf(result.day);
            ButtonToday.Content = result.name;
            ButtonToday.IsEnabled = false;
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
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
            AddingSection.ResetAndClear();
            monthFromMemory = 0;
            monthToMemory = 0;
            ButtonAdd.IsEnabled = false;
            ButtonClear.IsEnabled = false;
            ComboBoxCopy1.IsEnabled = false;
            ComboBoxCopy2.IsEnabled = false;
            ComboBoxCopy3.IsEnabled = false;
            ComboBoxYearTo.IsEnabled = false;
            ComboBoxMonthTo.IsEnabled = false;
            ComboBoxDayTo.IsEnabled = false;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var lesson = Model.CreateLesson(InputSubject.Text, InputTeacher.Text, InputAuditorium.Text, ComboBoxStartTime.SelectedIndex, AddingSection.GetEndTimeIndex((string)ComboBoxEndTime.SelectedValue), $"{ComboBoxStartTime.SelectedValue} - {ComboBoxEndTime.SelectedValue}");
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
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
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
            AddingSection.ResetAndClear();
            monthFromMemory = 0;
            monthToMemory = 0;
            ButtonAdd.IsEnabled = false;
            ButtonClear.IsEnabled = false;
            ComboBoxCopy1.IsEnabled = false;
            ComboBoxCopy2.IsEnabled = false;
            ComboBoxCopy3.IsEnabled = false;
            ComboBoxYearTo.IsEnabled = false;
            ComboBoxMonthTo.IsEnabled = false;
            ComboBoxDayTo.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex>=1 && ComboBoxMonthFrom.SelectedIndex >= 1 && ComboBoxDayFrom.SelectedIndex >= 1)
            {
                ComboBoxYearTo.IsEnabled=true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
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
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                    return;
                }
                if (InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex>-1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex>-1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex==-1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = true;
                        ComboBoxCopy2.IsEnabled = true;
                        ComboBoxCopy3.IsEnabled = true;
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
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
            }
        }
        private void InputTeacher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex >= 1 && ComboBoxMonthFrom.SelectedIndex >= 1 && ComboBoxDayFrom.SelectedIndex >= 1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
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
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                    return;
                }
                if (InputSubject.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = true;
                        ComboBoxCopy2.IsEnabled = true;
                        ComboBoxCopy3.IsEnabled = true;
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
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
            }
        }
        private void InputAuditorium_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex >= 1 && ComboBoxMonthFrom.SelectedIndex >= 1 && ComboBoxDayFrom.SelectedIndex >= 1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
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
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                    return;
                }
                if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 && ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
                {
                    if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                    {
                        ButtonAdd.IsEnabled = false;
                        ComboBoxCopy1.IsEnabled = false;
                        ComboBoxCopy2.IsEnabled = false;
                        ComboBoxCopy3.IsEnabled = false;
                    }
                    else
                    {
                        ButtonAdd.IsEnabled = true;
                        ComboBoxCopy1.IsEnabled = true;
                        ComboBoxCopy2.IsEnabled = true;
                        ComboBoxCopy3.IsEnabled = true;
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
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
            }
        }
        private void ComboBoxStartTime_DropDownClosed(object sender, EventArgs e)
        {
            AddingSection.SetEndTime(ComboBoxStartTime.SelectedIndex);
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1 &&
               ComboBoxYearTo.SelectedIndex > -1 && ComboBoxMonthTo.SelectedIndex > -1 && ComboBoxDayTo.SelectedIndex > -1)
            {
                if (AddingSection.FromExceedsTo((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue,
                                    (int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue))
                {
                    ComboBoxYearTo.SelectedIndex = -1;
                    AddingSection.ClearMonthsAndDates("to");
                    ButtonTargetDay.Content = string.Empty;
                }
            }
            if (ComboBoxYearFrom.SelectedIndex != -1)
            {
                AddingSection.SetMonthsFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue);
                AddingSection.ClearDates("from");
                ButtonToday.Content = "Today";
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1 &&
               ComboBoxYearTo.SelectedIndex > -1 && ComboBoxMonthTo.SelectedIndex > -1 && ComboBoxDayTo.SelectedIndex > -1)
            {
                if (AddingSection.FromExceedsTo((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue,
                                    (int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue))
                {
                    ComboBoxYearTo.SelectedIndex = -1;
                    AddingSection.ClearMonthsAndDates("to");
                    ButtonTargetDay.Content = string.Empty;
                }
            }
            if (ComboBoxYearFrom.SelectedIndex != -1 && ComboBoxMonthFrom.SelectedIndex != -1)
            {
                AddingSection.SetDatesFromDependOnCalendar((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), monthFromMemory);
                monthFromMemory = AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue);
                ButtonToday.Content = "Today";
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    if (AddingSection.FromExceedsTo((int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue,
                    (int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue))
                    {
                        if ((int)ComboBoxYearFrom.SelectedValue == (int)ComboBoxYearTo.SelectedValue)
                        {
                            ComboBoxYearFrom.SelectedIndex = -1;
                        }
                        AddingSection.ClearMonthsAndDates("to");
                    }
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                   ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (ComboBoxYearTo.SelectedIndex != -1)
            {
                AddingSection.ClearMonthsAndDates("to");
                ButtonTargetDay.Content = string.Empty;
                if ((int)ComboBoxYearFrom.SelectedValue==(int)ComboBoxYearTo.SelectedValue)
                {
                    AddingSection.SetMonthsToDependOnMonthsFrom(ComboBoxMonthFrom.SelectedIndex);
                }
                else
                {
                    AddingSection.SetMonthsToDependOnCalendar((int)ComboBoxYearTo.SelectedValue);
                }
                
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (ComboBoxYearTo.SelectedIndex != -1 && ComboBoxMonthTo.SelectedIndex != -1)
            {
                AddingSection.ClearDates("to");
                ButtonTargetDay.Content = string.Empty;
                if ((int)ComboBoxYearFrom.SelectedValue == (int)ComboBoxYearTo.SelectedValue&&(string)ComboBoxMonthFrom.SelectedValue == (string)ComboBoxMonthTo.SelectedValue)
                {
                    AddingSection.SetDatesToDependOnDatesFrom(ComboBoxMonthFrom.SelectedIndex);
                }
                else
                {
                    AddingSection.SetDatesToDependOnCalendar((int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), monthToMemory);
                }
                
                monthToMemory = AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue);
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
            if (InputSubject.Text.Length > 0 && InputTeacher.Text.Length > 0 && InputAuditorium.Text.Length > 0 && ComboBoxStartTime.SelectedIndex > -1 && ComboBoxEndTime.SelectedIndex > -1 &&
                  ComboBoxYearFrom.SelectedIndex > -1 && ComboBoxMonthFrom.SelectedIndex > -1 && ComboBoxDayFrom.SelectedIndex > -1)
            {
                if (ComboBoxYearTo.SelectedIndex == -1 && ComboBoxMonthTo.SelectedIndex == -1 && ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;                  
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
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
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            if (ComboBoxYearFrom.SelectedIndex >= -1 && ComboBoxMonthFrom.SelectedIndex >= -1 && ComboBoxDayFrom.SelectedIndex >= -1)
            {
                ComboBoxYearTo.IsEnabled = true;
                ComboBoxMonthTo.IsEnabled = true;
                ComboBoxDayTo.IsEnabled = true;
            }
            else
            {
                ComboBoxYearTo.IsEnabled = false;
                ComboBoxMonthTo.IsEnabled = false;
                ComboBoxDayTo.IsEnabled = false;
            }
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
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else if (ComboBoxYearTo.SelectedIndex == -1 || ComboBoxMonthTo.SelectedIndex == -1 || ComboBoxDayTo.SelectedIndex == -1)
                {
                    ButtonAdd.IsEnabled = false;
                    ComboBoxCopy1.IsEnabled = false;
                    ComboBoxCopy2.IsEnabled = false;
                    ComboBoxCopy3.IsEnabled = false;
                }
                else
                {
                    ButtonAdd.IsEnabled = true;
                    ComboBoxCopy1.IsEnabled = true;
                    ComboBoxCopy2.IsEnabled = true;
                    ComboBoxCopy3.IsEnabled = true;
                }
                ButtonClear.IsEnabled = true;
            }
            else
            {
                ComboBoxCopy1.IsEnabled = false;
                ComboBoxCopy2.IsEnabled = false;
                ComboBoxCopy3.IsEnabled = false;
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
            ComboBoxEndTime_DropDownClosed(sender, e);       
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
                    Border0.Background = new SolidColorBrush(Colors.Linen);
                    Border1.Background = new SolidColorBrush(Colors.White);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Linen);
                    Border2.Background = new SolidColorBrush(Colors.White);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 2:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border2.Background = new SolidColorBrush(Colors.Linen);
                    Border3.Background = new SolidColorBrush(Colors.White);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 3:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border2.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border3.Background = new SolidColorBrush(Colors.Linen);
                    Border4.Background = new SolidColorBrush(Colors.White);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 4:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border2.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border3.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border4.Background = new SolidColorBrush(Colors.Linen);
                    Border5.Background = new SolidColorBrush(Colors.White);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 5:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border2.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border3.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border4.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border5.Background = new SolidColorBrush(Colors.Linen);
                    Border6.Background = new SolidColorBrush(Colors.White);
                    break;
                case 6:
                    Border0.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border1.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border2.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border3.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border4.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border5.Background = new SolidColorBrush(Colors.Gainsboro);
                    Border6.Background = new SolidColorBrush(Colors.Linen);
                    break;
                default:
                    break;
            }
        }
        private void FutureBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.White);
            Border1.Background = new SolidColorBrush(Colors.White);
            Border2.Background = new SolidColorBrush(Colors.White);
            Border3.Background = new SolidColorBrush(Colors.White);
            Border4.Background = new SolidColorBrush(Colors.White);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void PastBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Gainsboro);
            Border3.Background = new SolidColorBrush(Colors.Gainsboro);
            Border4.Background = new SolidColorBrush(Colors.Gainsboro);
            Border5.Background = new SolidColorBrush(Colors.Gainsboro);
            Border6.Background = new SolidColorBrush(Colors.Gainsboro);
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            string navigateUri = link.NavigateUri.ToString();
            Process.Start("explorer",navigateUri);
            e.Handled = true;
        }
        private void Image_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("explorer", "https://online.top-academy.ru/");
        }
        private Card ConvertLessonToCard(Lesson lesson) 
        {
            var card = new Card();
            var stack = new StackPanel();
            var header = new TextBlock { Text = lesson.Subject };
            var text = new TextBlock { Text = $"{lesson.Teacher}\n{lesson.Auditorium}\n{lesson.Time}" };
            header.FontWeight = FontWeights.Heavy;

            if (lesson.PositionInDayEnd <= 7)
            {
                header.FontSize = 11;
                text.FontSize = 11;
            }
            if (lesson.PositionInDayEnd < 4)
            {
                header.FontSize = 11;
                stack.Children.Add(header);
            }
            else
            {
                stack.Children.Add(header);
                stack.Children.Add(text);
            }
            card.Content = stack;

            card.HorizontalContentAlignment = HorizontalAlignment.Center;
            card.VerticalContentAlignment = VerticalAlignment.Center;
            ElevationAssist.SetElevation(card, Elevation.Dp6);
            Grid.SetColumn(card, lesson.PositionInWeek);
            Grid.SetRow(card, lesson.PositionInDayStart);
            Grid.SetRowSpan(card, lesson.PositionInDayEnd);
            return card;
        }
        private void AddCardsToMondayGrid(Day monday)
        {
            GridMonday.Children.Clear();
            foreach (var item in monday.Lessons)
            {
                GridMonday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToTuesdayGrid(Day tuesday)
        {
            GridTuesday.Children.Clear();
            foreach (var item in tuesday.Lessons)
            {
                GridTuesday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToWednesdayGrid(Day wednesday)
        {
            GridWednesday.Children.Clear();
            foreach (var item in wednesday.Lessons)
            {
                GridWednesday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToThursdayGrid(Day thursday)
        {
            GridThursday.Children.Clear();
            foreach (var item in thursday.Lessons)
            {
                GridThursday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToFridayGrid(Day friday)
        {
            GridFriday.Children.Clear();
            foreach (var item in friday.Lessons)
            {
                GridFriday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToSaturdayGrid(Day saturday)
        {
            GridSaturday.Children.Clear();
            foreach (var item in saturday.Lessons)
            {
                GridSaturday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToSundayGrid(Day sunday)
        {
            GridSunday.Children.Clear();
            foreach (var item in sunday.Lessons)
            {
                GridSunday.Children.Add(ConvertLessonToCard(item));
            }
        }
        private void AddCardsToGrid(Day monday, Day tuesday, Day wednesday, Day thursday, Day friday, Day saturday, Day sunday)
        {
            AddCardsToMondayGrid(monday);
            AddCardsToTuesdayGrid(tuesday);
            AddCardsToWednesdayGrid(wednesday);
            AddCardsToThursdayGrid(thursday);
            AddCardsToFridayGrid(friday);
            AddCardsToSaturdayGrid(saturday);
            AddCardsToSundayGrid(sunday);
        }
    }
}

﻿using MaterialDesignThemes.Wpf;
using Schedule.Model;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            Model = new();
            AddingSection = new();
            ButtonBackContent = "<<";
            ButtonForwardContent =">>";
            InitializeComponent();
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
            HighligthedBackground(Model.GetCurrentDayIndex());


        }

        //public void TestFunction ()
        //{
        //    var text = new Card();
        //    text.Content = "Sex Lesson";
        //    text.HorizontalContentAlignment= HorizontalAlignment.Center;
        //    Grid.SetColumn(text, 2);
        //    Grid.SetRow(text, 12);
        //    Grid.SetRowSpan(text, 20);
        //    GridFriday.Children.Add(text);
        //}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.PreviousWeek();
            if (result.isCurrentWeek) HighligthedBackground(result.index);
            else DefaultBackground();
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
            if (result.isCurrentWeek) HighligthedBackground(result.index);
            else DefaultBackground();
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
            AddingSection.SetDatesFromDependOnCalendar(ComboBoxYearFrom.SelectedIndex + 2022, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue));
            ComboBoxDayFrom.SelectedIndex = ComboBoxDayFrom.Items.IndexOf(result.day);
            ButtonToday.Content = result.name;
            ButtonToday.IsEnabled = false;
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
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //TODO прописать проверки!!!!!!!!!
            var lesson = Model.CreateLesson(InputSubject.Text, InputTeacher.Text, InputAuditorium.Text, ComboBoxStartTime.SelectedIndex, ComboBoxEndTime.SelectedIndex, $"{ComboBoxStartTime.SelectedValue} - {ComboBoxEndTime.SelectedValue}");
            Model.AddLessonToDays(lesson, (int)ComboBoxYearFrom.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue,
                                            (int)ComboBoxYearTo.SelectedValue, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue, ComboBoxCopy1.SelectedIndex, ComboBoxCopy2.SelectedIndex, ComboBoxCopy3.SelectedIndex);
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
        }

        private void ComboBoxCopy1_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxCopy1.SelectedIndex > -1)
            {
                ComboBoxCopy2.Visibility = Visibility.Visible;
            }               
        }

        private void ComboBoxYearFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex != -1)
            {
                AddingSection.SetMonthsFromDependOnCalendar(ComboBoxYearFrom.SelectedIndex + 2022);
            }
        }

        private void ComboBoxMonthFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearFrom.SelectedIndex != -1 && ComboBoxMonthFrom.SelectedIndex != -1)
            {
                AddingSection.SetDatesFromDependOnCalendar(ComboBoxYearFrom.SelectedIndex + 2022, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue));
            }            
        }

        private void ComboBoxDayFrom_DropDownClosed(object sender, EventArgs e)
        {
            ChangeButtonTodayContent();
        }

        private void ComboBoxYearTo_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearTo.SelectedIndex != -1)
            {
                AddingSection.SetMonthsToDependOnCalendar(ComboBoxYearTo.SelectedIndex + 2022);
            }
        }

        private void ComboBoxMonthTo_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxYearTo.SelectedIndex != -1 && ComboBoxMonthTo.SelectedIndex != -1)
            {
                AddingSection.SetDatesToDependOnCalendar(ComboBoxYearTo.SelectedIndex + 2022, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue));
            }         
        }

        private void ComboBoxDayTo_DropDownClosed(object sender, EventArgs e)
        {
            ChangeButtonTargetDayContent();
        }
        private void ChangeButtonTodayContent()
        {
            if (ComboBoxYearFrom.SelectedIndex!=-1&& ComboBoxMonthFrom.SelectedIndex!=-1&& ComboBoxDayFrom.SelectedIndex!=-1)
            {
                ButtonToday.Content = AddingSection.GetSelectedDay(ComboBoxYearFrom.SelectedIndex + 2022, AddingSection.MonthToInt((string)ComboBoxMonthFrom.SelectedValue), (int)ComboBoxDayFrom.SelectedValue);
                ButtonToday.IsEnabled = false;
            }           
        }
        private void ChangeButtonTargetDayContent()
        {
            if (ComboBoxYearTo.SelectedIndex != -1 && ComboBoxMonthTo.SelectedIndex != -1 && ComboBoxDayTo.SelectedIndex != -1)
            {
                ButtonTargetDay.Content = AddingSection.GetSelectedDay(ComboBoxYearTo.SelectedIndex + 2022, AddingSection.MonthToInt((string)ComboBoxMonthTo.SelectedValue), (int)ComboBoxDayTo.SelectedValue);
            }
        }

        private void ComboBoxCopy2_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxCopy2.SelectedIndex > -1)
            {
                ComboBoxCopy3.Visibility = Visibility.Visible;
            }
        }
        private void HighligthedBackground(int column)
        {
            switch (column)
            {
                case 0:
                    GridMonday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 1:
                    GridTuesday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 2:
                    GridWednesday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 3:
                    GridThursday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 4:
                    GridFriday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 5:
                    GridSaturday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                case 6:
                    GridSunday.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 100, 150, 100));
                    break;
                default:
                    break;
            }
        }
        private void DefaultBackground()
        {
            GridMonday.Background = null;
            GridTuesday.Background = null;
            GridWednesday.Background = null;
            GridThursday.Background = null;
            GridFriday.Background = null;
            GridSaturday.Background = null;
            GridSunday.Background = null;
        }
    }
}

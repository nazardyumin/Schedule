using MaterialDesignThemes.Wpf;
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

        private Timer _timer;
        public MainWindow()
        {
            Model = new();
            AddingSection = new();
            ButtonBackContent = "<<";
            ButtonForwardContent =">>";
            SetTimer();
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
            else if (result.isFuture) FutureBackground(result.index);
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
            else if (result.isFuture) FutureBackground(result.index);
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
                    Border0.Background=new SolidColorBrush(Colors.LightYellow);
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
        private void DefaultBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border1.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border2.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border3.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border4.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border5.Background = new SolidColorBrush(Colors.WhiteSmoke);
            Border6.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }

        private void SetTimer()
        {
            var timeSpan = new TimeSpan(1, 0, 0, 0);
            DateTime tmp = DateTime.Now + timeSpan;
            var tomorrow=new DateTime(tmp.Year,tmp.Month,tmp.Day,0,0,0,0);
            DateTime now = DateTime.Now;
            var timeToNewDay = new TimeSpan(tomorrow.Ticks - now.Ticks);
            _timer = new Timer(timeToNewDay.TotalMilliseconds);
            _timer.Start();
            _timer.Elapsed += Timer_ChangeDay!;
        }

        private void Timer_ChangeDay(Object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            Model.FocuseOnCurrentWeek();
            Model.AddAllCardsToMondayGrid(ref GridMonday);
            Model.AddAllCardsToTuesdayGrid(ref GridTuesday);
            Model.AddAllCardsToWednesdayGrid(ref GridWednesday);
            Model.AddAllCardsToThursdayGrid(ref GridThursday);
            Model.AddAllCardsToFridayGrid(ref GridFriday);
            Model.AddAllCardsToSaturdayGrid(ref GridSaturday);
            Model.AddAllCardsToSundayGrid(ref GridSunday);
            HighligthedBackground(Model.GetCurrentDayIndex());
            SetTimer();
        }
    }
}

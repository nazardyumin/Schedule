using ExtraTools;
using MaterialDesignThemes.Wpf;
using Schedule.Models;
using Schedule.ViewModels;
using Schedule.Views.Cards;
using Schedule.Views.MenuItems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Schedule.Views.Windows.ScheduleWindow
{
    public partial class ScheduleWindow : Window
    {
        public WeekTemplate Model { get; set; }
        public AddingSectionTemplate AddingSection { get; set; }
        public string ButtonBackContent { get; set; }
        public string ButtonForwardContent { get; set; }

        public ScheduleWindow()
        {
            Model = new();
            AddingSection = new();
            ButtonBackContent = "<<";
            ButtonForwardContent = ">>";
            InitializeComponent();
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
            TodayBackground(Model.GetCurrentDayIndex());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var (isCurrentWeek, isFuture, index) = Model.PreviousWeek();
            if (isCurrentWeek) TodayBackground(index);
            else if (isFuture) FutureBackground();
            else PastBackground();
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
        }
        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            var (isCurrentWeek, isFuture, index) = Model.NextWeek();
            if (isCurrentWeek) TodayBackground(index);
            else if (isFuture) FutureBackground();
            else PastBackground();
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var setup = AddingSection.GetSetupInfo();
            var dateToCheck = AddingSection.GetFromValues();
            if (Model.IsOverlay(dateToCheck.year, dateToCheck.month, dateToCheck.day, setup.startTimeIndex, setup.endTimeIndex))
            {
                DialogBox.Show("ERROR", "Please change the lesson time to avoid overlays!", "OK");
            }
            else
            {
                var lesson = Model.CreateLesson(setup.subject, setup.teacher, setup.auditorium, setup.startTimeIndex, setup.endTimeIndex, setup.duration, setup.date);
                if (AddingSection.IsPeriodSelected())
                {
                    var (yearFrom, monthFrom, dayFrom, yearTo, monthTo, dayTo, copy1, copy2, copy3) = AddingSection.GetFromAndToValues();
                    Model.AddLessonToDays(lesson, yearFrom, monthFrom, dayFrom, yearTo, monthTo, dayTo, copy1, copy2, copy3);
                }
                else
                {
                    var (year, month, day) = AddingSection.GetFromValues();
                    Model.AddLessonToOneDay(lesson, year, month, day);
                }
                AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
                AddingSection.CommandClearFunction();
                ComboBoxCopy2.Visibility = Visibility.Hidden;
                ComboBoxCopy3.Visibility = Visibility.Hidden;
            }
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxCopy2.Visibility = Visibility.Hidden;
            ComboBoxCopy3.Visibility = Visibility.Hidden;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var (subject, teacher, auditorium, startTimeIndex, endTimeIndex, duration, date, dayIndex, lessonIndex) = AddingSection.GetSetupInfo();
            var (year, month, day) = AddingSection.GetFromValues();
            Model.EditLesson(dayIndex, lessonIndex, subject, teacher, auditorium, startTimeIndex, endTimeIndex, duration, date, year, month, day);
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
            AddingBlock.Visibility = Visibility.Visible;
            EditingBlock.Visibility = Visibility.Hidden;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            AddingBlock.Visibility = Visibility.Visible;
            EditingBlock.Visibility = Visibility.Hidden;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MyMenuItem)e.Source;
            var connection = menuItem.GetConnectionIndexes();
            var lesson = Model.GetSelectedLesson(connection.dayIndex, connection.lessonIndex);
            AddingSection.Editor(lesson);
            AddingBlock.Visibility = Visibility.Hidden;
            EditingBlock.Visibility = Visibility.Visible;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var res = DialogBox.Show("DELETE", "Are you shure?", "YES","NO");
            switch (res)
            {
                case DialogBox.Result_.LeftButtonClicked:
                    var menuItem = (MyMenuItem)e.Source;
                    var connection = menuItem.GetConnectionIndexes();
                    Model.DeleteSelectedLesson(connection.dayIndex, connection.lessonIndex);
                    AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
                    break;
                case DialogBox.Result_.RightButtonClicked:
                    break;
            }
        }
        private void ComboBoxCopy1_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxCopy2.Visibility = Visibility.Visible;
        }
        private void ComboBoxCopy2_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxCopy3.Visibility = Visibility.Visible;
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            string navigateUri = link.NavigateUri.ToString();
            Process.Start("explorer", navigateUri);
            e.Handled = true;
        }
        private void TodayBackground(int column)
        {
            switch (column)
            {
                case 0:
                    TodayIsMondayBackground();
                    break;
                case 1:
                    TodayIsTuesdayBackground();
                    break;
                case 2:
                    TodayIsWednesdayBackground();
                    break;
                case 3:
                    TodayIsThursdayBackground();
                    break;
                case 4:
                    TodayIsFridayBackground();
                    break;
                case 5:
                    TodayIsSaturdayBackground();
                    break;
                case 6:
                    TodayIsSundayBackground();
                    break;
                default:
                    break;
            }
        }
        private void TodayIsMondayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Linen);
            Border1.Background = new SolidColorBrush(Colors.White);
            Border2.Background = new SolidColorBrush(Colors.White);
            Border3.Background = new SolidColorBrush(Colors.White);
            Border4.Background = new SolidColorBrush(Colors.White);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsTuesdayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Linen);
            Border2.Background = new SolidColorBrush(Colors.White);
            Border3.Background = new SolidColorBrush(Colors.White);
            Border4.Background = new SolidColorBrush(Colors.White);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsWednesdayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Linen);
            Border3.Background = new SolidColorBrush(Colors.White);
            Border4.Background = new SolidColorBrush(Colors.White);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsThursdayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Gainsboro);
            Border3.Background = new SolidColorBrush(Colors.Linen);
            Border4.Background = new SolidColorBrush(Colors.White);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsFridayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Gainsboro);
            Border3.Background = new SolidColorBrush(Colors.Gainsboro);
            Border4.Background = new SolidColorBrush(Colors.Linen);
            Border5.Background = new SolidColorBrush(Colors.White);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsSaturdayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Gainsboro);
            Border3.Background = new SolidColorBrush(Colors.Gainsboro);
            Border4.Background = new SolidColorBrush(Colors.Gainsboro);
            Border5.Background = new SolidColorBrush(Colors.Linen);
            Border6.Background = new SolidColorBrush(Colors.White);
        }
        private void TodayIsSundayBackground()
        {
            Border0.Background = new SolidColorBrush(Colors.Gainsboro);
            Border1.Background = new SolidColorBrush(Colors.Gainsboro);
            Border2.Background = new SolidColorBrush(Colors.Gainsboro);
            Border3.Background = new SolidColorBrush(Colors.Gainsboro);
            Border4.Background = new SolidColorBrush(Colors.Gainsboro);
            Border5.Background = new SolidColorBrush(Colors.Gainsboro);
            Border6.Background = new SolidColorBrush(Colors.Linen);
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
        private MyCard ConvertLessonToCard(Lesson lesson)
        {
            var card = new MyCard();
            card.SetConnectionIndexes(lesson.ConnectionDayIndex, lesson.ConnectionLessonIndex);
            SetContent(ref card, lesson);
            SetStyle(ref card, lesson);
            return card;
        }
        private void SetContent(ref MyCard card, Lesson lesson)
        {
            var stack = new StackPanel();
            var header = new TextBlock { Text = $"Subject: {lesson.Subject}" };
            var text = new TextBlock { Text = $"Teacher: {lesson.Teacher}\nAuditorium: {lesson.Auditorium}\n{lesson.Duration}" };
            header.FontWeight = FontWeights.Heavy;
            if (lesson.PositionInDayEnd <= 7)
            {
                header.FontSize = 11;
                text.FontSize = 11;
            }
            if (lesson.PositionInDayEnd < 4)
            {
                stack.Children.Add(header);
            }
            else
            {
                stack.Children.Add(header);
                stack.Children.Add(text);
            }
            card.Content = stack;
        }
        private void SetStyle(ref MyCard card, Lesson lesson)
        {
            card.Margin = new Thickness(2, 0, 2, 0);
            card.HorizontalContentAlignment = HorizontalAlignment.Center;
            card.VerticalContentAlignment = VerticalAlignment.Center;
            ElevationAssist.SetElevation(card, Elevation.Dp6);
            Grid.SetColumn(card, lesson.PositionInWeek);
            Grid.SetRow(card, lesson.PositionInDayStart);
            Grid.SetRowSpan(card, lesson.PositionInDayEnd);
        }
        private void SetColorMenuAndEvents(ref MyCard card, Day day)
        {
            if (day.IsPast())
            {

                card.Background = new SolidColorBrush(Colors.WhiteSmoke);
                card.Foreground = new SolidColorBrush(Colors.Gray);
                card.MouseEnter += CardPast_MouseEnter;
                card.MouseLeave += CardPast_MouseLeave;
            }
            else if (day.IsFuture())
            {
                card.Background = new SolidColorBrush(Colors.Ivory);
                SetContextMenu(ref card);
                card.MouseEnter += CardFuture_MouseEnter;
                card.MouseLeave += CardFuture_MouseLeave;
            }
            else
            {
                card.Background = new SolidColorBrush(Colors.Wheat);
                SetContextMenu(ref card);
                card.MouseEnter += CardToday_MouseEnter;
                card.MouseLeave += CardToday_MouseLeave;
            }
        }
        private void SetContextMenu(ref MyCard card)
        {
            var (dayIndex, lessonIndex) = card.GetConnectionIndexes();

            var edit = new MyMenuItem
            {
                Header = "Edit"
            };
            edit.SetConnectionIndexes(dayIndex, lessonIndex);
            edit.Click += Edit_Click;
            edit.Style = (Style)ResDic["StyleMenuItem"];

            var delete = new MyMenuItem
            {
                Header = "Delete"
            };
            delete.SetConnectionIndexes(dayIndex, lessonIndex);
            delete.Click += Delete_Click;
            delete.Style = (Style)ResDic["StyleMenuItem"];

            var items = new List<MenuItem>() { edit, delete };
            var contextMenu = new ContextMenu
            {
                ItemsSource = items,
                FontSize = 13
            };
            card.ContextMenu = contextMenu;
        }
        private void CardFuture_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Background = new SolidColorBrush(Colors.Ivory);
            ((MyCard)sender).Margin = new Thickness(2, 0, 2, 0);
            ((MyCard)sender).FontSize = 13;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp6);
        }
        private void CardFuture_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Background = new SolidColorBrush(Colors.LightYellow);
            ((MyCard)sender).Margin = new Thickness(0);
            ((MyCard)sender).FontSize = 13.1;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp16);
        }
        private void CardToday_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Background = new SolidColorBrush(Colors.Wheat);
            ((MyCard)sender).Margin = new Thickness(2, 0, 2, 0);
            ((MyCard)sender).FontSize = 13;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp6);
        }
        private void CardToday_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Background = new SolidColorBrush(Colors.Moccasin);
            ((MyCard)sender).Margin = new Thickness(0);
            ((MyCard)sender).FontSize = 13.1;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp16);
        }
        private void CardPast_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Margin = new Thickness(2, 0, 2, 0);
            ((MyCard)sender).FontSize = 13;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp6);
        }
        private void CardPast_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((MyCard)sender).Margin = new Thickness(0);
            ((MyCard)sender).FontSize = 13.1;
            ElevationAssist.SetElevation((MyCard)sender, Elevation.Dp16);
        }
        private void AddCardsToMondayGrid(Day monday)
        {
            GridMonday.Children.Clear();
            foreach (var item in monday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, monday);
                GridMonday.Children.Add(card);
            }
        }
        private void AddCardsToTuesdayGrid(Day tuesday)
        {
            GridTuesday.Children.Clear();
            foreach (var item in tuesday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, tuesday);
                GridTuesday.Children.Add(card);
            }
        }
        private void AddCardsToWednesdayGrid(Day wednesday)
        {
            GridWednesday.Children.Clear();
            foreach (var item in wednesday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, wednesday);
                GridWednesday.Children.Add(card);
            }
        }
        private void AddCardsToThursdayGrid(Day thursday)
        {
            GridThursday.Children.Clear();
            foreach (var item in thursday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, thursday);
                GridThursday.Children.Add(card);
            }
        }
        private void AddCardsToFridayGrid(Day friday)
        {
            GridFriday.Children.Clear();
            foreach (var item in friday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, friday);
                GridFriday.Children.Add(card);
            }
        }
        private void AddCardsToSaturdayGrid(Day saturday)
        {
            GridSaturday.Children.Clear();
            foreach (var item in saturday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, saturday);
                GridSaturday.Children.Add(card);
            }
        }
        private void AddCardsToSundayGrid(Day sunday)
        {
            GridSunday.Children.Clear();
            foreach (var item in sunday.Lessons!)
            {
                var card = ConvertLessonToCard(item);
                SetColorMenuAndEvents(ref card, sunday);
                GridSunday.Children.Add(card);
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

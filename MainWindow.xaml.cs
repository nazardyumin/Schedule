using MaterialDesignThemes.Wpf;
using Schedule.Model;
using Schedule.View;
using Schedule.ViewModel;
using System;
using System.Diagnostics;
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
        public MainWindow()
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
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var setup = AddingSection.GetSetupInfo();
            var lesson = Model.CreateLesson(setup.subject, setup.teacher, setup.auditorium, setup.startTimeIndex, setup.endTimeIndex, setup.duration);
            var isPeriodSelected = AddingSection.IsPeriodSelected();
            if (isPeriodSelected)
            {
                var dates = AddingSection.GetFromAndToValues();
                Model.AddLessonToDays(lesson, dates.yearFrom, dates.monthFrom, dates.dayFrom, dates.yearTo, dates.monthTo, dates.dayTo, dates.copy1, dates.copy2, dates.copy3);
            }
            else
            {
                var date = AddingSection.GetFromValues();
                Model.AddLessonToOneDay(lesson, date.year, date.month, date.day);
            }
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
            AddingSection.CommandClearFunction();
            ComboBoxCopy2.Visibility = Visibility.Hidden;
            ComboBoxCopy3.Visibility = Visibility.Hidden;
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxCopy2.Visibility = Visibility.Hidden;
            ComboBoxCopy3.Visibility = Visibility.Hidden;
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

        private MyCard ConvertLessonToCard(Lesson lesson)
        {
            var card = new MyCard();
            card.SetConnectionIndexes(lesson.ConnectionDayIndex,lesson.ConnectionLessonIndex);
            var stack = new StackPanel();
            var header = new TextBlock { Text = $"Subject: {lesson.Subject}" };
            var text = new TextBlock { Text = $"Teacher: {lesson.Teacher}\nAuditorium: {lesson.Auditorium}\n{lesson.Time}" };
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
            card.MouseRightButtonUp += MyCard_MouseDoubleClick; //testing!!!!!!!!
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
        private void DeleteCardFromGrid(int dayIndex, MyCard card)
        {
            switch (dayIndex)
            {
                case 0:
                    GridMonday.Children.Remove(card);
                    break;
                case 1:
                    GridTuesday.Children.Remove(card);
                    break;
                case 2:
                    GridWednesday.Children.Remove(card);
                    break;
                case 3:
                    GridThursday.Children.Remove(card);
                    break;
                case 4:
                    GridFriday.Children.Remove(card);
                    break;
                case 5:
                    GridSaturday.Children.Remove(card);
                    break;
                case 6:
                    GridSunday.Children.Remove(card);
                    break;
            }
        }

        //testing!!!!
        private void MyCard_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var indexes = ((MyCard)sender).GetConnectionIndexes();
            Model.DeleteSelectedLesson(indexes.dayIndex, indexes.lessonIndex);
            AddCardsToGrid(Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday, Model.Sunday);
        }
    }
}

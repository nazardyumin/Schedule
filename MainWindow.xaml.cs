using MaterialDesignThemes.Wpf;
using Schedule.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Schedule
{
    public partial class MainWindow : Window
    {
        public WeekTemplate Model { get; set; }
        public string ButtonBackContent { get; set; }
        public string ButtonForwardContent { get; set; }
        public MainWindow()
        {
            Model = new();
            ButtonBackContent = "<<";
            ButtonForwardContent =">>";
            InitializeComponent();
            TestFunction();
            
        }

        public void TestFunction ()
        {
            var text = new Card();
            text.Content = "Sex Lesson";
            text.HorizontalContentAlignment= HorizontalAlignment.Center;
            Grid.SetColumn(text, 2);
            Grid.SetRow(text, 12);
            Grid.SetRowSpan(text, 20);
            GridFriday.Children.Add(text);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Model.PreviousWeek();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            Model.NextWeek();
        }

        private void ButtonToday_Click(object sender, RoutedEventArgs e)
        {
            var result = Model.GetTodayIndexesAndDay();
            ComboBoxYearFrom.SelectedIndex = result.year;
            ComboBoxMonthFrom.SelectedIndex = result.month;
            ComboBoxDayFrom.SelectedIndex = result.day;
            ButtonToday.Content = result.name;
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
            ComboBoxCopy1.SelectedIndex = -1;
            ComboBoxCopy2.SelectedIndex = -1;
            ComboBoxCopy2.Visibility = Visibility.Hidden;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //TODO прописать проверки!!!!!!!!!
            //var newLesson = new Lesson (InputSubject.Text, InputTeacher.Text, InputAuditorium.Text, ComboBoxStartTime.SelectedValue.ToString(), ComboBoxEndTime.SelectedValue.ToString());
        }

        private void ComboBoxCopy1_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxCopy2.Visibility = Visibility.Visible;
        }
    }
}

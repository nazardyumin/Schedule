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
            var result = Model.GetTodayIndexes();
            ComboBoxYear.SelectedIndex = result.year;
            ComboBoxMonth.SelectedIndex = result.month;
            ComboBoxDay.SelectedIndex = result.day;
        }
    }
}

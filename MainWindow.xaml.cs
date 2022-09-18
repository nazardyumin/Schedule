using MaterialDesignThemes.Wpf;
using Schedule.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Schedule
{
    public partial class MainWindow : Window
    {
        public WeekTemplate SelectedWeek { get; set; }
        public char ButtonBackContent { get; set; }
        public char ButtonForwardContent { get; set; }
        public MainWindow()
        {
            SelectedWeek = new();
            ButtonBackContent = '<';
            ButtonForwardContent ='>';
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
            SelectedWeek.PreviousWeek();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek.NextWeek();
        }
    }
}

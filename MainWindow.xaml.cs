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
        public WeekTemplate Test { get; set; }
        public MainWindow()
        {
            Test = new();
            InitializeComponent();
            TestFunction();
            SelectedWeek = new();
            var test = SelectedWeek.Sunday;
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
    }
}

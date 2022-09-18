using MaterialDesignThemes.Wpf;
using Schedule.Model;
using System.Windows;
using System.Windows.Controls;

namespace Schedule
{
    public partial class MainWindow : Window
    {
        public ScheduleWeekTemplate Test { get; set; }
        public MainWindow()
        {
            Test = new();
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
    }
}

using Schedule.Model;
using System.Windows;

namespace Schedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ScheduleWeek Test { get; set; }
        public MainWindow()
        {
            Test = new();
            InitializeComponent();
        }
    }
}

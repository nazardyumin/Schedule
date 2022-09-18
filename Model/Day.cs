using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;

namespace Schedule.Model
{
    public class Day
    {
        public DateTime Date { get; set; }
        public ObservableCollection<Card>? Lessons { get; set; }
    }
}

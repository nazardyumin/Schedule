using Schedule.Models;

namespace Schedule.ViewModels.Bindings
{
    public class WeekTemplateDayInfoBinding : Notifier
    {
        private string[] _shortDayInfos = new string[7];
        public string[] Short
        {
            get => _shortDayInfos;
            set => SetField(ref _shortDayInfos, value);
        }

        public void Set(string[] infos)
        {
            Short = infos;
        }
    }
}

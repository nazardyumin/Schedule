using Schedule.Models;

namespace Schedule.ViewModels.Week
{
    public class WeekStateBinding : Notifier
    {
        private bool _canPressForward;
        public bool CanPressForward
        {
            get => _canPressForward;
            set => SetField(ref _canPressForward, value);
        }

        private bool _canPressBack;
        public bool CanPressBack
        {
            get => _canPressBack;
            set => SetField(ref _canPressBack, value);
        }

        private bool _canPressCurrentWeek;
        public bool CanPressCurrentWeek
        {
            get => _canPressCurrentWeek;
            set => SetField(ref _canPressCurrentWeek, value);
        }

        private int _stepsFromCurrentWeek;
        public int StepsFromCurrentWeek
        {
            get => _stepsFromCurrentWeek;
            set
            {
                SetField(ref _stepsFromCurrentWeek, value);
                RefreshCanPressCurrentWeekState();
            }
        }

        public WeekStateBinding()
        {
            CanPressForward = true;
            CanPressBack = true;
            StepsFromCurrentWeek = 0;
        }

        public void RefreshCanPressCurrentWeekState()
        {
            CanPressCurrentWeek = StepsFromCurrentWeek is >= 2 or <= -2;
        }
    }
}

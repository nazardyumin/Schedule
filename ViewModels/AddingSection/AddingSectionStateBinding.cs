using Schedule.Models;

namespace Schedule.ViewModels.AddingSection
{
    public class AddingSectionStateBinding : Notifier
    {
        private bool _canPressAdd;
        public bool CanPressAdd
        {
            get => _canPressAdd;
            set => SetField(ref _canPressAdd, value);
        }

        private bool _canPressClear;
        public bool CanPressClear
        {
            get => _canPressClear;
            set => SetField(ref _canPressClear, value);
        }

        private bool _canPressCopy;
        public bool CanPressCopy
        {
            get => _canPressCopy;
            set => SetField(ref _canPressCopy, value);
        }

        private bool _canPressToday;
        public bool CanPressToday
        {
            get => _canPressToday;
            set => SetField(ref _canPressToday, value);
        }

        private bool _canPressTo;
        public bool CanPressTo
        {
            get => _canPressTo;
            set => SetField(ref _canPressTo, value);
        }

        private bool _canPressSave;
        public bool CanPressSave
        {
            get => _canPressSave;
            set => SetField(ref _canPressSave, value);
        }

        private bool _isAddingMode;
        public bool IsAddingMode
        {
            get => _isAddingMode;
            set => SetField(ref _isAddingMode, value);
        }

        public AddingSectionStateBinding()
        {
            CanPressToday = true;
            CanPressAdd = false;
            CanPressClear = false;
            CanPressCopy = false;
            CanPressTo = false;
            CanPressSave = true;
            IsAddingMode = true;
        }
    }
}

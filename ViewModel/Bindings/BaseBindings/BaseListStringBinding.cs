using Schedule.Model;

namespace Schedule.ViewModel.Bindings.BaseBindings
{
    public abstract class BaseListStringBinding : Notifier
    {
        protected string _selectedValue;
        public string SelectedValue
        {
            get => _selectedValue;
            set => SetField(ref _selectedValue, value);
        }

        protected int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetField(ref _selectedIndex, value);
        }

        protected BaseListStringBinding()
        {
            _selectedValue = string.Empty;
            _selectedIndex = -1;
        }

        public bool IsOk()
        {
            return _selectedIndex != -1;
        }
    }
}

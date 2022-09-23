using Schedule.Model;

namespace Schedule.ViewModel.Bindings
{
    public class ListStringBinding : Notifier
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
            set
            {
                SetField(ref _selectedIndex, value);
                IsAllOk();
            }
        }

        protected bool _isOk;
        public bool IsOk
        {
            get => _isOk;
            set => SetField(ref _isOk, value);
        }
        public ListStringBinding()
        {
            _selectedValue = string.Empty;
            _selectedIndex = -1;
        }

        protected void IsAllOk()
        {
            if (SelectedIndex != -1) IsOk = true;
            else IsOk = false;
        }
    }
}

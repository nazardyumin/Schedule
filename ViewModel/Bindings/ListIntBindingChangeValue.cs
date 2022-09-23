using System;

namespace Schedule.ViewModel.Bindings
{
    public class ListIntBindingChangeValue : ListIntBinding
    {
        private readonly Action _action;
        public new int SelectedValue
        {
            get => _selectedValue;
            set
            {
                SetField(ref _selectedValue, value);
                _action.Invoke();
            }
        }
        public ListIntBindingChangeValue(Action action)
        {
            _action = action;
        }
    }
}

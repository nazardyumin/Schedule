using System;

namespace Schedule.ViewModel.Bindings
{
    public class ListStringBindingChangeValue : ListStringBinding
    {
        private readonly Action _action;
        public new string SelectedValue
        {
            get => _selectedValue;
            set
            {
                SetField(ref _selectedValue, value);
                _action.Invoke();
            }
        }
        public ListStringBindingChangeValue(Action action)
        {
            _action = action;
        }
    }
}

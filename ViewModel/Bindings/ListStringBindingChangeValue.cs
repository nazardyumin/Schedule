using System;

namespace Schedule.ViewModel.Bindings
{
    public class ListStringBindingChangeValue : ListStringBinding
    {
        private readonly Action _action;
        public new string Value
        {
            get => _value;
            set
            {
                SetField(ref _value, value);
                _action.Invoke();
            }
        }
        public ListStringBindingChangeValue(Action action)
        {
            _action = action;
        }
    }
}

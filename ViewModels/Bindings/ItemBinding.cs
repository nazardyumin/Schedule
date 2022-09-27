using Schedule.Models;
using System;

namespace Schedule.ViewModels.Bindings
{
    public class ItemBinding : Notifier
    {
        private string _value;
        private readonly Action _action;
        public string Value
        {
            get => _value;
            set
            {
                SetField(ref _value, value);
                _action.Invoke();
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                SetField(ref _index, value);
                IsAllOk();
            }
        }

        private bool _isOk;
        public bool IsOk
        {
            get => _isOk;
            set => SetField(ref _isOk, value);
        }

        public ItemBinding(Action action)
        {
            _value = string.Empty;
            _index = -1;
            _action = action;
        }

        private void IsAllOk()
        {
            if (Index != -1) IsOk = true;
            else IsOk = false;
        }
        public int ValueToInt()
        {
            if (_value is not null)
            {
                return int.Parse(_value);
            }
            else
            {
                return -1;
            }
        }
    }
}

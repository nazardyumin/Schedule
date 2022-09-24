using Schedule.Model;
using System;

namespace Schedule.ViewModel.Bindings
{
    public class InputBinding : Notifier
    {
        private readonly Action _action;

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                SetField(ref _value, value);
                IsAllOk();
                _action.Invoke();
            }
        }

        private bool _isOk;
        public bool IsOk
        {
            get => _isOk;
            set => SetField(ref _isOk, value);
        }

        public InputBinding(Action action)
        {
            _value = string.Empty;
            _action = action;
        }

        private void IsAllOk()
        {
            var isNotEmpty = string.IsNullOrEmpty(Value);
            var check = Value.ToCharArray();
            bool hasLetters = false;
            foreach (var item in check)
            {
                hasLetters = char.IsLetterOrDigit(item);
                if (hasLetters) break;
            }
            if (!isNotEmpty && hasLetters) IsOk = true;
            else IsOk = false;
        }
    }
}

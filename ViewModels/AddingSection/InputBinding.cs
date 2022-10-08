using Schedule.Models;
using System;

namespace Schedule.ViewModels.AddingSection
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
            var isEmpty = string.IsNullOrEmpty(Value);
            var check = Value.ToCharArray();
            var hasLetters = false;
            foreach (var item in check)
            {
                hasLetters = char.IsLetterOrDigit(item);
                if (hasLetters) break;
            }

            if (!isEmpty && hasLetters) IsOk = true;
            else IsOk = false;
        }
    }
}
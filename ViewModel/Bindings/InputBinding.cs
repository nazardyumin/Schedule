using Schedule.Model;

namespace Schedule.ViewModel.Bindings
{
    public class InputBinding : Notifier
    {
        private string enteredValue;
        public string EnteredValue
        {
            get => enteredValue;
            set
            {
                SetField(ref enteredValue, value);
                IsAllOk();
            }
        }

        private bool _isOk;
        public bool IsOk
        {
            get => _isOk;
            set => SetField(ref _isOk, value);
        }

        public InputBinding()
        {
            enteredValue = string.Empty;
        }
        private void IsAllOk()
        {
            var isNotEmpty = string.IsNullOrEmpty(EnteredValue);
            var check = EnteredValue.ToCharArray();
            bool hasLetters = false;
            foreach (var item in check)
            {
                hasLetters = char.IsLetter(item);
                if (hasLetters) break;
            }
            if (isNotEmpty && hasLetters) IsOk = true;
            else IsOk = false;
        }
    }
}

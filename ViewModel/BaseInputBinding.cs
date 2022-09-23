using Schedule.Model;

namespace Schedule.ViewModel
{
    public abstract class BaseInputBinding : Notifier
    {
        protected string enteredValue;
        public string EnteredValue
        {
            get => enteredValue!;
            set => SetField(ref enteredValue, value);
        }
        protected BaseInputBinding()
        {
            enteredValue = string.Empty;
        }
        protected bool IsNotEmpty()
        {
            return !string.IsNullOrEmpty(enteredValue);
        }
        protected bool HasLetters()
        {
            var check = enteredValue.ToCharArray();
            bool hasLetters = false;
            foreach (var item in check)
            {
                hasLetters = char.IsLetter(item);
                if (hasLetters) return hasLetters;
            }
            return hasLetters;
        }
        public bool IsOk()
        {
            return IsNotEmpty() && HasLetters();
        }
    }
}

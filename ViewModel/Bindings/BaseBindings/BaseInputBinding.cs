using Schedule.Model;

namespace Schedule.ViewModel.Bindings.BaseBindings
{
    public abstract class BaseInputBinding : Notifier
    {
        protected string enteredValue;
        public string EnteredValue
        {
            get => enteredValue;
            set => SetField(ref enteredValue, value);
        }

        protected bool isOk;
        public bool IsOk
        {
            get => isOk;
            set => SetField(ref isOk, value);       
        }

        protected BaseInputBinding()
        {
            enteredValue = string.Empty;
        }

        protected bool HasLetters()
        {
            var check = EnteredValue.ToCharArray();
            bool hasLetters = false;
            foreach (var item in check)
            {
                hasLetters = char.IsLetter(item);
                if (hasLetters) return hasLetters;
            }
            return hasLetters;
        }
        protected void IsAllOk()
        {
            var isNotEmpty=string.IsNullOrEmpty(EnteredValue);
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

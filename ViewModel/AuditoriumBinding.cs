using Schedule.Model;

namespace Schedule.ViewModel
{
    public class AuditoriumBinding : Notifier
    {
        private string enteredValue;
        public string EnteredValue
        {
            get => enteredValue!;
            set => SetField(ref enteredValue, value);
        }
        public AuditoriumBinding()
        {
            enteredValue = string.Empty;
        }
    }
}

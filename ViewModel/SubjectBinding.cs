using Schedule.Model;

namespace Schedule.ViewModel
{
    public class SubjectBinding : Notifier
    {
        private string? enteredValue;
        public string EnteredValue
        {
            get => enteredValue!;
            set => SetField(ref enteredValue, value);
        }
        public SubjectBinding()
        {
            enteredValue = string.Empty;
        }
    }
}

using Schedule.Model;

namespace Schedule.ViewModel
{
    public class TeacherBinding : Notifier
    {
        private string? enteredValue;
        public string EnteredValue
        {
            get => enteredValue!;
            set => SetField(ref enteredValue, value);
        }
        public TeacherBinding()
        {
            enteredValue = string.Empty;
        }
    }
}

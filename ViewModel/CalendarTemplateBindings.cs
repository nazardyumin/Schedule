using Schedule.Model;

namespace Schedule.ViewModel
{
    public class CalendarTemplateBindings : Notifier
    {

        private bool canAdd;
        public bool CanAdd 
        { 
            get => canAdd; 
            set => SetField(ref canAdd, value); 
        }

        private bool canClear;
        public bool CanClear 
        { 
            get => canClear; 
            set => SetField(ref canClear, value);
        }
        private void IsAddingAvailable()
        {

        }
    }
}

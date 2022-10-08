using Schedule.Models;

namespace Schedule.ViewModels.AddingSection
{
    public class AddingSectionControlsContentBinding : Notifier
    {
        private string? _today;
        public string Today
        {
            get => _today!;
            set => SetField(ref _today, value);
        }

        private string? _targetDay;
        public string TargetDay
        {
            get => _targetDay!;
            set => SetField(ref _targetDay, value);
        }

        private string? _addOrSave;
        public string AddOrSave
        {
            get => _addOrSave!;
            set => SetField(ref _addOrSave, value);
        }
        public AddingSectionControlsContentBinding()
        {
            AddOrSave = "Add";
            Today = "Today";
            TargetDay = string.Empty;
        }

        public void ResetTargetDay()
        {
            TargetDay = string.Empty;
        }
        public void ResetToday()
        {
            Today = "Today";
        }
        public void AddingMode()
        {
            AddOrSave = "Add";
        }
        public void EditingMode()
        {
            AddOrSave = "Save";
        }
    }
}

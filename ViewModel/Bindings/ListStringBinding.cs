using Schedule.Model;

namespace Schedule.ViewModel.Bindings
{
    public class ListStringBinding : Notifier
    {
        protected string _value;
        public string Value
        {
            get => _value;
            set => SetField(ref _value, value);
        }

        protected int _index;
        public int Index
        {
            get => _index;
            set
            {
                SetField(ref _index, value);
                IsAllOk();
            }
        }

        protected bool _isOk;
        public bool IsOk
        {
            get => _isOk;
            set => SetField(ref _isOk, value);
        }
        public ListStringBinding()
        {
            _value = string.Empty;
            _index = -1;
        }

        protected void IsAllOk()
        {
            if (Index != -1) IsOk = true;
            else IsOk = false;
        }
        public int ValueToInt()
        {
            return int.Parse(_value);
        }
    }
}

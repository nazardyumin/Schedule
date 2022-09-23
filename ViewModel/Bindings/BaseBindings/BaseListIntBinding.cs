using Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.ViewModel.Bindings.BaseBindings
{
    public class BaseListIntBinding : Notifier
    {
        protected int _selectedValue;
        public int SelectedValue
        {
            get => _selectedValue;
            set => SetField(ref _selectedValue, value);
        }

        protected int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetField(ref _selectedIndex, value);
        }

        protected BaseListIntBinding()
        {
            _selectedIndex = -1;
        }

        public bool IsOk()
        {
            return _selectedIndex != -1;
        }
    }
}

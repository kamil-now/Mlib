namespace Mlib.UI.Additional
{
    using Caliburn.Micro;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ListCommandItem : PropertyChangedBase, IContextMenuItem
    {
        private bool isSelected;

        public string Name { get; }
        public ICommand Command { get; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                NotifyOfPropertyChange();
            }
        }
        public ListCommandItem(string name, ICommand command)
        {
            Name = name;
            Command = command;
        }
    }
}

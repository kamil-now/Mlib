using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mlib.UI.Additional
{
    public class ListActionItem
    {
        public string Name { get; }
        public ICommand Command { get; }
        public ListActionItem(string name, ICommand command)
        {
            Name = name;
            Command = command;
        }
    }
}

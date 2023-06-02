using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RiSC.MainWindowMVVM
{
    class MainWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<object> Action { get; set; }

        Func<object,bool> Canexcute { get; set; }

        public bool CanExecute(object parameter)
        {
            if (Canexcute == null)
            {
                return true;
            }
            else return Canexcute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
          this.Action.Invoke(parameter);
        }

        public MainWindowCommand(Action<object> action)
        {
            this.Action = action;
            this.Canexcute = null;
        }

        MainWindowCommand(Action<object> action,Func<object,bool> func)
        {
            this.Action = action;
            this.Canexcute = func;
        }
    }
}

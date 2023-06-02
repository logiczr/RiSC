using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RiSC.MainPageMVVM
{
    internal class MainPageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> ExcuteAction { get; set; }

        public Func<object,bool> Canexcute { get; set; }

        public bool CanExecute(object parameter)
        {
            if(Canexcute == null) { return true;}
            return Canexcute.Invoke(parameter);   
        }

        public void Execute(object parameter)
        {
           ExcuteAction?.Invoke(parameter);
        }

        public MainPageCommand(Action<object> action) 
        {
            this.ExcuteAction = action;
            this.Canexcute = null;
        }
    }
}

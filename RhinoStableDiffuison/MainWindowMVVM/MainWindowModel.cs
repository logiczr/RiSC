using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RiSC.MainWindowMVVM
{
    class MainWindowModel
    {
        public MainPage MainPage { get; set; }=new MainPage();

        public SettingPage SettingPage{ get; set; } = new SettingPage();

        public ControlNetPage ControlNetPage { get; set; } = new ControlNetPage();

        public DeveloperPage DeveloperPage { get; set; } = new DeveloperPage();

        public Page CurrentPage { get; set; }
    }
}

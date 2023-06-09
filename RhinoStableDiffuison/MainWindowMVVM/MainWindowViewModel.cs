using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using Rhino;
using RiSC.MainPageMVVM;

namespace RiSC.MainWindowMVVM
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowModel MainModel { get; set; }

        public void NotifyPropertyChanged(string PropName) 
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
            MainModel= new MainWindowModel();
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            MainModel.CurrentPage = MainModel.MainPage;
            MainModel.MainPage.DataContext = mainPageViewModel;
            MainModel.ControlNetPage.DataContext = mainPageViewModel;
            MainModel.SettingPage.DataContext = mainPageViewModel;
            MainModel.DeveloperPage.DataContext= mainPageViewModel;
            MainModel.IntroDuctionPage.DataContext= mainPageViewModel;
        }

        public Page FramePage { get { return MainModel.CurrentPage; } set { MainModel.CurrentPage = value; NotifyPropertyChanged("FramePage"); } }

        internal Window window;

        public ICommand CloseWindow { get { return new MainWindowCommand((args) => { this.window.Close(); }); } }

        public ICommand ToHomePage { get { return new MainWindowCommand((args) => { FramePage = MainModel.MainPage; }); } }

        public ICommand ToControlNetPage { get { return new MainWindowCommand((args) => { FramePage = MainModel.ControlNetPage; }); } }

        public ICommand ToDeveloperPage { get { return new MainWindowCommand((args) => { FramePage = MainModel.DeveloperPage; }); } }

        public ICommand ToSettingPage { get { return new MainWindowCommand((args) => { FramePage = MainModel.SettingPage; }); } }

        public ICommand IntroDuctionPage { get { return new MainWindowCommand((args) => { FramePage = MainModel.IntroDuctionPage; }); } }

        public ICommand DragMove { get { return new MainWindowCommand((args) => { window.DragMove(); }); } }
    }
}

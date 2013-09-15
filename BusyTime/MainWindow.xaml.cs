using BusyTime.Model;
using BusyTime.Repository;
using BusyTime.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace BusyTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainVM;
        private IWorksRepository worksRepo;

        public MainWindow()
        {
            InitializeComponent();

            worksRepo = new Repository.AppSettingImp.WorksRepository();
            mainVM = new MainViewModel(worksRepo.GetLast());

            MainV.DataContext = mainVM;
            mainVM.Clock.Mode = ClockModel.ClockMode.Viewed;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                case WindowState.Normal:
                    mainVM.Clock.Mode = ClockModel.ClockMode.Viewed;
                    break;
                case WindowState.Minimized:
                    mainVM.Clock.Mode = ClockModel.ClockMode.Slowed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("WindowState", WindowState, "Hell Has Frozen Over... Can not understand this state of the Window.");
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            worksRepo.StoreAsLast(mainVM.Work);
        }
    }
}

using BusyTime.ViewModel;
using System;
using System.Windows;

namespace BusyTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainVM;

        public MainWindow()
        {
            InitializeComponent();

            mainVM = new MainViewModel();
            MainV.DataContext = mainVM;
            mainVM.ClockStart();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                case WindowState.Normal:
                    mainVM.ClockStart();
                    break;
                case WindowState.Minimized:
                    mainVM.ClockStop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("WindowState", WindowState, "Hell Has Frozen Over... Can not understand this state of the Window.");
            }
        }
    }
}

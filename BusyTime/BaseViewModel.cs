using System.ComponentModel;

namespace BusyTime
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            PropertyChanged += (s, e) => { };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Windows.Threading;

namespace BusyTime.Model
{
    class ClockModel
    {
        private DispatcherTimer timer;
        private ClockMode mode;

        public ClockMode Mode
        {
            get { return mode; }
            set
            {
                if (mode != value)
                {
                    mode = value;

                    switch (mode)
                    {
                        case ClockMode.Viewed:
                            timer.Interval = TimeSpan.FromSeconds(0.1);
                            timer.Start();
                            break;
                        case ClockMode.Slowed:
                            timer.Interval = TimeSpan.FromSeconds(1.0);
                            timer.Start();
                            break;
                        case ClockMode.Dispatched:
                            timer.Interval = TimeSpan.FromMinutes(2.5);
                            timer.Start();
                            break;
                        case ClockMode.Terminated:
                            timer.Stop();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value", value, "Can not understand this value...");
                    }
                }
            }
        }

        public ClockModel(Dispatcher dispatcher, EventHandler tickHandler)
        {
            mode = ClockMode.Terminated;

            timer = new DispatcherTimer(DispatcherPriority.DataBind, dispatcher);
            timer.IsEnabled = false;
            timer.Tick += tickHandler;
        }
    }
}

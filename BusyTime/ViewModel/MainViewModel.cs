using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BusyTime.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public DateTime WorkStart { get; private set; }
        private int workStartHours;
        public int WorkStartHours
        {
            get { return workStartHours; }
            set
            {
                if (value != workStartHours)
                {
                    workStartHours = value;
                    DateTime now = DateTime.Now;
                    WorkStart = new DateTime(now.Year, now.Month, now.Day, workStartHours, WorkStart.Minute, 0);

                    Notify("WorkStartHours");
                    Notify("WorkStart");

                    UpdateEndOfWork();
                }
            }
        }
        private int workStartMinutes;
        public int WorkStartMinutes
        {
            get { return workStartMinutes; }
            set
            {
                if (value != workStartMinutes)
                {
                    workStartMinutes = value;
                    DateTime now = DateTime.Now;
                    WorkStart = new DateTime(now.Year, now.Month, now.Day, WorkStart.Hour, workStartMinutes, 0);

                    Notify("WorkStartMinutes");
                    Notify("WorkStart");
                    UpdateEndOfWork();
                }
            }
        }


        public TimeSpan WorkTime { get; private set; }
        private double workTimeHours;
        public double WorkTimeHours
        {
            get { return workTimeHours; }
            set
            {
                if (value != workTimeHours)
                {
                    workTimeHours = value;
                    WorkTime = TimeSpan.FromHours(workTimeHours);

                    Notify("WorkTimeHours");
                    Notify("WorkTime");
                    UpdateEndOfWork();
                }
            }
        }


        public DateTime WorkEnd { get; private set; }
        private void UpdateEndOfWork()
        {
            WorkEnd = WorkStart.Add(WorkTime);

            Notify("WorkEnd");
            UpdateRemainingTime();
        }


        public TimeSpan RemainingTime { get; private set; }
        private void UpdateRemainingTime()
        {
            RemainingTime = WorkEnd.Subtract(DateTime.Now);
            Notify("RemainingTime");
        }

        private DispatcherTimer clockTimer;
        public MainViewModel()
        {
            WorkStart = DateTime.Now;
            workStartHours = WorkStart.Hour;
            workStartMinutes = WorkStart.Minute;

            workTimeHours = 8.5;
            WorkTime = TimeSpan.FromHours(workTimeHours);

            WorkEnd = WorkStart.Add(WorkTime);
            RemainingTime = WorkEnd.Subtract(DateTime.Now);
        }
    }
}

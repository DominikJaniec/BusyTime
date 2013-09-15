using System;
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


        public TimeSpan DeclaredWorkTime { get; private set; }
        private double declaredWorkTimeHours;
        public double DeclaredWorkTimeHours
        {
            get { return declaredWorkTimeHours; }
            set
            {
                if (value != declaredWorkTimeHours)
                {
                    declaredWorkTimeHours = value;
                    DeclaredWorkTime = TimeSpan.FromHours(declaredWorkTimeHours);

                    Notify("DeclaredWorkTimeHours");
                    Notify("DeclaredWorkTime");
                    UpdateEndOfWork();
                }
            }
        }


        public DateTime WorkEnd { get; private set; }
        private void UpdateEndOfWork()
        {
            WorkEnd = WorkStart.Add(DeclaredWorkTime);

            Notify("WorkEnd");
            RecalculateResultTimes();
        }


        public TimeSpan CurrentWorkingTime { get; private set; }
        public double CurrentTimePercent { get; private set; }
        public TimeSpan RemainingTime { get; private set; }
        public bool IsOverWork { get; private set; }
        private void RecalculateResultTimes()
        {
            DateTime nowDateTime = DateTime.Now;
            bool riseIOverWorkEvent = false;

            CurrentWorkingTime = nowDateTime.Subtract(WorkStart);
            CurrentTimePercent = CurrentWorkingTime.TotalSeconds / DeclaredWorkTime.TotalSeconds;

            if (CurrentTimePercent < 1.0)
            {
                RemainingTime = WorkEnd.Subtract(nowDateTime);
                riseIOverWorkEvent = false ^ IsOverWork;
                IsOverWork = false;
            }
            else
            {
                RemainingTime = nowDateTime.Subtract(WorkEnd);
                riseIOverWorkEvent = true ^ IsOverWork;
                IsOverWork = true;
            }

            Notify("CurrentWorkingTime");
            Notify("CurrentTimePercent");
            Notify("RemainingTime");

            if (riseIOverWorkEvent)
            {
                Notify("IsOverWork");
            }
        }


        private DispatcherTimer ClockInWork;
        private void ClockInWorkTick(object sender, EventArgs e)
        {
            RecalculateResultTimes();
        }

        public void ClockStart()
        {
            ClockInWork.Start();
        }
        public void ClockStop()
        {
            ClockInWork.Stop();
        }


        public MainViewModel()
        {
            DateTime nowDateTime = DateTime.Now;

            WorkStart = nowDateTime;
            workStartHours = WorkStart.Hour;
            workStartMinutes = WorkStart.Minute;

            declaredWorkTimeHours = 8.5;
            DeclaredWorkTime = TimeSpan.FromHours(declaredWorkTimeHours);

            WorkEnd = WorkStart.Add(DeclaredWorkTime);
            RemainingTime = WorkEnd.Subtract(nowDateTime);

            CurrentWorkingTime = nowDateTime.Subtract(WorkStart);
            CurrentTimePercent = CurrentWorkingTime.TotalSeconds / DeclaredWorkTime.TotalSeconds;

            if (CurrentTimePercent < 1.0)
            {
                IsOverWork = false;
                RemainingTime = WorkEnd.Subtract(nowDateTime);
            }
            else
            {
                IsOverWork = true;
                RemainingTime = nowDateTime.Subtract(WorkEnd);
            }

            ClockInWork = new DispatcherTimer();
            ClockInWork.Tick += ClockInWorkTick;
            ClockInWork.Interval = TimeSpan.FromSeconds(0.1);
        }
    }
}
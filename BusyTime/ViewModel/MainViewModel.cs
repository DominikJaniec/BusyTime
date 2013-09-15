using BusyTime.Model;
using System;
using System.Windows.Threading;

namespace BusyTime.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public WorkModel Work { get; private set; }
        public ClockModel Clock { get; private set; }

        public MainViewModel(WorkModel model)
        {
            Initialize(model);
        }
        public MainViewModel()
        {
            Initialize(new WorkModel
            {
                Start = DateTime.Now.AddMinutes(-5.0),
                Time = TimeSpan.FromHours(8.5)
            });
        }

        private void Initialize(WorkModel model)
        {
            Work = model;

            workStartHours = Work.Start.Hour;
            workStartMinutes = Work.Start.Minute;
            declaredWorkTimeHours = Work.Time.TotalHours;

            Clock = new ClockModel(Dispatcher.CurrentDispatcher, new EventHandler((s, e) => RecalculateResultTimes()));
        }

        private int workStartHours;
        public int WorkStartHours
        {
            get { return workStartHours; }
            set
            {
                if (value != workStartHours)
                {
                    workStartHours = value;
                    Work.Start = Work.Start.Date.AddHours(workStartHours).AddMinutes(Work.Start.Minute);

                    Notify("WorkStartHours");
                    UpdateWorkAndRecalculate();
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
                    Work.Start = Work.Start.Date.AddHours(Work.Start.Hour).AddMinutes(workStartMinutes);

                    Notify("WorkStartMinutes");
                    UpdateWorkAndRecalculate();
                }
            }
        }

        private double declaredWorkTimeHours;
        public double DeclaredWorkTimeHours
        {
            get { return declaredWorkTimeHours; }
            set
            {
                if (value != declaredWorkTimeHours)
                {
                    declaredWorkTimeHours = value;
                    Work.Time = TimeSpan.FromHours(declaredWorkTimeHours);

                    Notify("DeclaredWorkTimeHours");
                    UpdateWorkAndRecalculate();
                }
            }
        }

        private void UpdateWorkAndRecalculate()
        {
            Notify("Work");
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

            CurrentWorkingTime = nowDateTime.Subtract(Work.Start);
            CurrentTimePercent = CurrentWorkingTime.TotalSeconds / Work.Time.TotalSeconds;

            if (CurrentTimePercent < 1.0)
            {
                RemainingTime = Work.Finish.Subtract(nowDateTime);
                riseIOverWorkEvent = false ^ IsOverWork;
                IsOverWork = false;
            }
            else
            {
                RemainingTime = nowDateTime.Subtract(Work.Finish);
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
    }
}
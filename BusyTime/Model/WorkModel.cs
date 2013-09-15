using System;

namespace BusyTime.Model
{
    class WorkModel
    {
        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                Finish = start.Add(Time);
            }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                Finish = Start.Add(time);
            }
        }

        public DateTime Finish { get; private set; }
    }
}

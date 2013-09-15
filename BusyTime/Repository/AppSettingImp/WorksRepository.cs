using BusyTime.Model;
using BusyTime.Properties;
using System;

namespace BusyTime.Repository.AppSettingImp
{
    class WorksRepository : IWorksRepository
    {
        public int GetLastHours()
        {
            return Settings.Default.WorkHours;
        }
        public int GetLastMinutes()
        {
            return Settings.Default.WorkMinutes;
        }
        public double GetLastTime()
        {
            return Settings.Default.WorkTime;
        }

        public WorkModel GetLast()
        {
            return new WorkModel
            {
                Start = DateTime.Now.Date
                        .AddHours(Settings.Default.WorkHours)
                        .AddMinutes(Settings.Default.WorkMinutes),
                Time = TimeSpan.FromHours(Settings.Default.WorkTime)
            };
        }
        public void StoreAsLast(WorkModel model)
        {
            Settings.Default.WorkHours = model.Start.Hour;
            Settings.Default.WorkMinutes = model.Start.Minute;
            Settings.Default.WorkTime = model.Time.TotalHours;

            Settings.Default.Save();
        }
    }
}

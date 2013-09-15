using BusyTime.Model;

namespace BusyTime.Repository
{
    interface IWorksRepository
    {
        int GetLastHours();
        int GetLastMinutes();
        double GetLastTime();

        WorkModel GetLast();
        void StoreAsLast(WorkModel model);
    }
}

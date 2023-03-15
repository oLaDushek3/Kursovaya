using System.Collections.Generic;

namespace Kursovaya.Model.Worker
{
    public interface IWorkerRepository
    {
        void Add(WorkerModel workerModel);
        void Edit(WorkerModel workerModel);
        void Remove(int id);
        IEnumerable<WorkerModel> GetByAll();
    }
}

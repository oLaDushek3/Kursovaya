using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Worker
{
    public interface IWorkerRepository
    {
        void Add(WorkerModel workerModel);
        void Edit(WorkerModel workerModel);
        void Remove(int id);
        List<WorkerModel> GetByAll(ApplicationContext context);
        WorkerModel GetById(int id, ApplicationContext context);
    }
}

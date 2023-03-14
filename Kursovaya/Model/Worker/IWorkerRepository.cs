using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

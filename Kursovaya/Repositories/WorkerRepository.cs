using Kursovaya.Model;
using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kursovaya.Repositories
{
    public class WorkerRepository : ApplicationContext, IWorkerRepository
    {
        public void Add(WorkerModel workerModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(WorkerModel workerModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkerModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

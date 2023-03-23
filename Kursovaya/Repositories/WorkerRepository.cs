﻿using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public List<WorkerModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public WorkerModel GetById(int id, ApplicationContext context)
        {
            WorkerModel? worker = context.Workers.
                Include(w => w.Post).FirstOrDefault(w => w.WorkerId == id);

            return worker;
        }
    }
}

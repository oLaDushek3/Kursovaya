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

        public void Remove(int id, ApplicationContext context)
        {
            WorkerModel workerModel = context.Workers.Where(w => w.WorkerId == id).FirstOrDefault();
            context.Workers.Remove(workerModel);
            context.SaveChanges();
        }
        public WorkerModel GetById(int id, ApplicationContext context)
        {
            WorkerModel worker = context.Workers.
                Include(w => w.Post).First(w => w.WorkerId == id);

            return worker;
        }
        public List<WorkerModel> GetByAll(ApplicationContext context)
        {
            List<WorkerModel> workers = context.Workers.
                Include(w => w.Post).
                Include(w => w.Supplies).ToList();
            return workers;
        }
            }
}

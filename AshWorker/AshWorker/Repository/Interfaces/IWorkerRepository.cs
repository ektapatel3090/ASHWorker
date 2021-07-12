using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AshWorker.Models;

namespace AshWorker.Repository.Interfaces
{
    public interface IWorkerRepository
    {
        Task<List<Worker>> GetWorkers();
        Task<Worker> CreateWorker(Worker request);
    }
}

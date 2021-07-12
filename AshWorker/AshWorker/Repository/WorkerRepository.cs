using AshWorker.Context;
using AshWorker.Models;
using AshWorker.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AshWorker.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkerDBContext _context;
        public WorkerRepository(WorkerDBContext context)
        {
            _context = context;
        }

        public async Task<Worker> CreateWorker(Worker request)
        {

            Worker worker = new Worker();
            _context.Set<Worker>().Add(request);
            await _context.SaveChangesAsync();
            return request;

        }

        public async Task<List<Worker>> GetWorkers()
        {

            List<Worker> list = await _context.Set<Worker>().ToListAsync(); // Gets inforamtion from DB;
            return list;

        }

    }
}

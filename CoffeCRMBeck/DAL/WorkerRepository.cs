using System;
using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.DAL.@interface;


namespace CoffeCRMBeck.DAL
{
	public class WorkerRepository : IRepository<Worker>
    {
        private readonly AppDbContext _db;

        public WorkerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Worker model)
        {
            if (model != null)
            {
                await _db.Workers.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Edit(Worker model)
        {
            try
            {
                if (model != null)
                {
                    _db.Workers.Update(model);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IQueryable<Worker> GetAll()
        {
            return _db.Workers;
        }
    }
}


using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.DAL
{
    public class CheckRepository : IRepository<Сheck>
    {
        private readonly AppDbContext _db;

        public CheckRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> CreateAsync(Сheck model)
        {
            try
            {
                if (model != null)
                {
                    await _db.Сhecks.AddAsync(model);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> EditAsync(Сheck model)
        {
            try
            {
                if (model != null)
                {
                    _db.Сhecks.Update(model);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IQueryable<Сheck> GetAll()
        {
            return _db.Сhecks.Include(p => p.Products).Include(p => p.Worker);
        }

    }
}


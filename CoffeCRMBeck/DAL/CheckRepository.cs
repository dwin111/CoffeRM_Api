using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.DAL
{
    public class CheckRepository : IRepository<Bill>
    {
        private readonly AppDbContext _db;

        public CheckRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> CreateAsync(Bill model)
        {
            try
            {
                if (model != null)
                {
                    await _db.Bill.AddAsync(model);
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
        public async Task<bool> EditAsync(Bill model)
        {
            try
            {
                if (model != null)
                {
                    _db.Bill.Update(model);
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

        public IQueryable<Bill> GetAll()
        {
            return _db.Bill.Include(p => p.Orders).Include(p => p.Staff);
        }

    }
}


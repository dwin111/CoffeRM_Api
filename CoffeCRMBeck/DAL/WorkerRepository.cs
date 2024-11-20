using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;


namespace CoffeCRMBeck.DAL
{
    public class WorkerRepository : IRepository<Staff>
    {
        private readonly AppDbContext _db;

        public WorkerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Staff model)
        {
            if (model != null)
            {
                await _db.Staff.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> EditAsync(Staff model)
        {
            try
            {
                if (model != null)
                {
                    _db.Staff.Update(model);
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

        public IQueryable<Staff> GetAll()
        {
            return _db.Staff;
        }
    }
}


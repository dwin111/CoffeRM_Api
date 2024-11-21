using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;


namespace CoffeCRMBeck.DAL
{
    public class ProductRepository : IRepository<Order>
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> CreateAsync(Order model)
        {
            if (model != null)
            {
                await _db.Order.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(Order model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditAsync(Order model)
        {
            try
            {
                if (model != null)
                {
                    _db.Order.Update(model);
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

        public IQueryable<Order> GetAll()
        {
            return _db.Order;
        }

        public Task<Order> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}


using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;


namespace CoffeCRMBeck.DAL
{
    public class ProductCatalogRepository : IRepository<Menu>
    {
        private readonly AppDbContext _db;

        public ProductCatalogRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> CreateAsync(Menu model)
        {
            if (model != null)
            {
                await _db.Menu.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> EditAsync(Menu model)
        {
            try
            {
                if (model != null)
                {
                    _db.Menu.Update(model);
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

        public IQueryable<Menu> GetAll()
        {
            return _db.Menu;
        }
    }
}


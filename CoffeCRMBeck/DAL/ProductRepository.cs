using System;
using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.DAL.@interface;


namespace CoffeCRMBeck.DAL
{
	public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> Create(Product model)
        {
            if (model != null)
            {
                await _db.Products.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Edit(Product model)
        {
            try
            {
                if (model != null)
                {
                    _db.Products.Update(model);
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

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}


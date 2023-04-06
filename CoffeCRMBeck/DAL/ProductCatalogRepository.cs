﻿using System;
using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.DAL.@interface;


namespace CoffeCRMBeck.DAL
{
	public class ProductCatalogRepository : IRepository<ProductCatalog>
    {
        private readonly AppDbContext _db;

        public ProductCatalogRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> Create(ProductCatalog model)
        {
            if (model != null)
            {
                await _db.ProductCatalogs.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Edit(ProductCatalog model)
        {
            try
            {
                if (model != null)
                {
                    _db.ProductCatalogs.Update(model);
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

        public IQueryable<ProductCatalog> GetAll()
        {
            return _db.ProductCatalogs;
        }
    }
}

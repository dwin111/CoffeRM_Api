using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.Services
{
    public class ProductCatalogService
    {

        private IRepository<ProductCatalog> _productCatalogRepository;

        public ProductCatalogService(IRepository<ProductCatalog> productCatalogRepository)
        {
            _productCatalogRepository = productCatalogRepository;
        }

        public async Task<List<ProductCatalog>> GetAll()
        {
            return await _productCatalogRepository.GetAll().ToListAsync();
        }

        public async Task<bool> Create(ProductCatalogViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return false;
                }

                foreach (var item in GetAll().Result)
                {
                    if (item.Name == productViewModel.Name)
                    {
                        return false;
                    }
                }

                var newModel = new ProductCatalog()
                {
                    Id = 0,
                    Name = productViewModel.Name,
                    ImageURL = productViewModel.ImageURL,
                    Price = productViewModel.Price,
                    Type = productViewModel.Type,
                };

                await _productCatalogRepository.Create(newModel);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ProductCatalog> GetById(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new ProductCatalog() { };
                }

                var model = _productCatalogRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (model == null)
                {
                    return new ProductCatalog() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new ProductCatalog() { };
            }
        }

        public async Task<bool> FullEdit(ProductCatalog model)
        {
            try
            {
                if (model == null)
                {
                    return false;
                }
                var productCatalogEditModel = await _productCatalogRepository.Edit(model);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}


using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.Services
{
    public class ProductCatalogService
    {

        private IRepository<Menu> _productCatalogRepository;

        public ProductCatalogService(IRepository<Menu> productCatalogRepository)
        {
            _productCatalogRepository = productCatalogRepository;
        }

        public async Task<List<Menu>> GetAllAsync()
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

                foreach (var item in GetAllAsync().Result)
                {
                    if (item.Name == productViewModel.Name)
                    {
                        return false;
                    }
                }

                var newModel = new Menu()
                {
                    Id = 0,
                    Name = productViewModel.Name,
                    ImageURL = productViewModel.ImageURL,
                    Price = productViewModel.Price,
                    Type = productViewModel.Type,
                };

                await _productCatalogRepository.CreateAsync(newModel);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Menu> GetByIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Menu() { };
                }

                var model = _productCatalogRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (model == null)
                {
                    return new Menu() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Menu() { };
            }
        }

        public async Task<bool> FullEditAsync(Menu model)
        {
            try
            {
                if (model == null)
                {
                    return false;
                }
                var productCatalogEditModel = await _productCatalogRepository.EditAsync(model);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}


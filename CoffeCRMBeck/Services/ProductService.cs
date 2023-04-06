using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.Enums;
using CoffeCRMBeck.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CoffeCRMBeck.Services
{
    public class ProductService
    {
        private IRepository<Сheck> _checkRepository;
        private IRepository<Worker> _workerRepository;
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Сheck> checkRepository, IRepository<Worker> workerRepository, IRepository<Product> productRepository)
        {
            _checkRepository = checkRepository;
            _workerRepository = workerRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAll()
        {
            return  _productRepository.GetAll().ToList();
        }

        public async Task<bool> Create(ProductViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return false;
                }

                var newModel= new Product()
                {
                    Id = 0,
                    Name  = productViewModel.Name,
                    Price = productViewModel.Price,
                    Type  = productViewModel.Type,
                };

                await _productRepository.Create(newModel);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Product> GetById(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Product() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (model == null)
                {
                    return new Product() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Product() { };
            }
        }

        public async Task<Product> GetByNameAndById(long Id, string name)
        {
            try
            {
                if (name.Count() <= 0)
                {
                    return new Product() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Name == name && ch.Id == Id);

                if (model == null)
                {
                    return new Product() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Product() { };
            }
        }

        public async Task<Product> GetByName(string name)
        {
            try
            {
                if (name.Count() <= 0)
                {
                    return new Product() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Name == name);

                if (model == null)
                {
                    return new Product() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Product() { };
            }
        }
    }
}

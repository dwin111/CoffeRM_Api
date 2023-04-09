using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;

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

        public async Task<List<Product>> GetAllAsync()
        {
            return _productRepository.GetAll().ToList();
        }

        public async Task<bool> CreateAsync(ProductViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return false;
                }

                var newModel = new Product()
                {
                    Id = 0,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    Type = productViewModel.Type,
                };

                await _productRepository.CreateAsync(newModel);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Product> GetByIdAsync(long Id)
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

        public async Task<Product> GetByNameAndByIdAsync(long Id, string name)
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

        public async Task<Product> GetByNameAsync(string name)
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

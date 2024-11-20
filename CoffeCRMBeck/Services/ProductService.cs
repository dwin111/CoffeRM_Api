using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;

namespace CoffeCRMBeck.Services
{
    public class ProductService
    {
        private IRepository<Bill> _checkRepository;
        private IRepository<Staff> _workerRepository;
        private IRepository<Order> _productRepository;

        public ProductService(IRepository<Bill> checkRepository, IRepository<Staff> workerRepository, IRepository<Order> productRepository)
        {
            _checkRepository = checkRepository;
            _workerRepository = workerRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Order>> GetAllAsync()
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

                var newModel = new Order()
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
        public async Task<Order> GetByIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Order() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (model == null)
                {
                    return new Order() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Order() { };
            }
        }

        public async Task<Order> GetByNameAndByIdAsync(long Id, string name)
        {
            try
            {
                if (name.Count() <= 0)
                {
                    return new Order() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Name == name && ch.Id == Id);

                if (model == null)
                {
                    return new Order() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Order() { };
            }
        }

        public async Task<Order> GetByNameAsync(string name)
        {
            try
            {
                if (name.Count() <= 0)
                {
                    return new Order() { };
                }

                var model = _productRepository.GetAll().FirstOrDefault(ch => ch.Name == name);

                if (model == null)
                {
                    return new Order() { };
                }

                return model;
            }
            catch (Exception ex)
            {
                return new Order() { };
            }
        }
    }
}

using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;

namespace CoffeCRMBeck.Services
{
    public class WorkerService
    {
        private IRepository<Сheck> _checkRepository;
        private IRepository<Worker> _workerRepository;
        private IRepository<Product> _productRepository;

        public WorkerService(IRepository<Сheck> checkRepository, IRepository<Worker> workerRepository, IRepository<Product> productRepository)
        {
            _checkRepository = checkRepository;
            _workerRepository = workerRepository;
            _productRepository = productRepository;
        }
        public async Task<List<Worker>> GetAllAsync()
        {
            return _workerRepository.GetAll().ToList();
        }

        public async Task<bool> CreateAsync(WorkerViewModel workerViewModel)
        {
            try
            {
                if (workerViewModel == null)
                {
                    return false;
                }

                var newModel = new Worker()
                {
                    Id = 0,
                    Name = workerViewModel.Name,
                    Email = workerViewModel.Email,
                    Phone = workerViewModel.Phone,
                    Roles = workerViewModel.Roles,
                    Password = workerViewModel.Password,
                };

                await _workerRepository.CreateAsync(newModel);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Worker> GetByIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Worker() { };
                }

                var chek = _workerRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (chek == null)
                {
                    return new Worker() { };
                }

                return chek;
            }
            catch (Exception ex)
            {
                return new Worker() { };
            }
        }
    }
}

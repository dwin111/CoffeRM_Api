using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;

namespace CoffeCRMBeck.Services
{
    public class WorkerService
    {
        private IRepository<Bill> _checkRepository;
        private IRepository<Staff> _workerRepository;
        private IRepository<Order> _productRepository;

        public WorkerService(IRepository<Bill> checkRepository, IRepository<Staff> workerRepository, IRepository<Order> productRepository)
        {
            _checkRepository = checkRepository;
            _workerRepository = workerRepository;
            _productRepository = productRepository;
        }
        public async Task<List<Staff>> GetAllAsync()
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

                var newModel = new Staff()
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
        public async Task<Staff> GetByIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Staff() { };
                }

                var chek = _workerRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (chek == null)
                {
                    return new Staff() { };
                }

                return chek;
            }
            catch (Exception ex)
            {
                return new Staff() { };
            }
        }
    }
}

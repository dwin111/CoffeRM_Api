using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;

namespace CoffeCRMBeck.Services
{
    public class CheckService
    {
        private IRepository<Bill> _checkRepository;
        private IRepository<Staff> _StaffRepository;
        private IRepository<Order> _productRepository;


        public CheckService(IRepository<Bill> checkRepository, IRepository<Staff> StaffRepository, IRepository<Order> productRepository)
        {
            _checkRepository = checkRepository;
            _StaffRepository = StaffRepository;
            _productRepository = productRepository;
        }

        public List<Bill> GetAll()
        {
            return _checkRepository.GetAll().ToList();
        }

        public async Task<bool> CreateAsync(CheckViewModel checkViewModel)
        {
            try
            {

                if (checkViewModel.Orders == null || checkViewModel.Orders.Count <= 0 || checkViewModel.Staff == null)
                {
                    return false;
                }

                float price = 0;
                foreach (var item in checkViewModel.Orders)
                {
                    price += item.Price;
                }

                var newCheck = new Bill()
                {
                    Id = 0,
                    Orders = checkViewModel.Orders,
                    Staff = checkViewModel.Staff,
                    Date = DateTime.UtcNow,
                    Price = price,
                };

                await _checkRepository.CreateAsync(newCheck);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Bill> GetByIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new Bill() { };
                }

                var chek = _checkRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if (chek == null)
                {
                    return new Bill() { };
                }

                return chek;
            }
            catch (Exception ex)
            {
                return new Bill() { };
            }
        }
        public async Task<List<Bill>> GetByProductIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new() { new Bill() { } };
                }
                var chek = _checkRepository.GetAll();


                if (chek == null)
                {
                    return new() { new Bill() { } };
                }

                return chek.ToList();
            }
            catch (Exception ex)
            {
                return new() { new Bill() { } };
            }
        }
        public async Task<List<Bill>> GetByStaffIdAsync(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new() { new Bill() { } };
                }
                var chek = _checkRepository.GetAll().Where(ch => ch.Staff.Id == Id);

                if (chek == null)
                {
                    return new() { new Bill() { } };
                }

                return chek.ToList();
            }
            catch (Exception ex)
            {
                return new() { new Bill() { } };
            }
        }
        public async Task<bool> EditAsync(Bill check)
        {
            try
            {
                if (check == null)
                {
                    return false;
                }
                await _checkRepository.EditAsync(check);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

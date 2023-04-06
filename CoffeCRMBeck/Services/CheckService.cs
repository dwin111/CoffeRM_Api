using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.Enums;
using CoffeCRMBeck.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CoffeCRMBeck.Services
{
    public class CheckService
    {
        private IRepository<Сheck> _checkRepository;
        private IRepository<Worker> _workerRepository;
        private IRepository<Product> _productRepository;


        public CheckService(IRepository<Сheck> checkRepository, IRepository<Worker> workerRepository, IRepository<Product> productRepository)
        {
            _checkRepository = checkRepository;
            _workerRepository = workerRepository;
            _productRepository = productRepository;
        }

        public List<Сheck> GetAll()
        {
            return  _checkRepository.GetAll().ToList();
        }

        public async Task<bool> Create(CheckViewModel checkViewModel)
        {
            try
            {
               
                if(checkViewModel.Products == null || checkViewModel.Products.Count <= 0 || checkViewModel.Worker == null) 
                {
                    return false;
                }

                float price = 0;
                foreach(var item in checkViewModel.Products) 
                {
                    price += item.Price;
                }

                var newCheck = new Сheck() 
                {
                    Id = 0,
                    Products = checkViewModel.Products,
                    Worker = checkViewModel.Worker,
                    Date = DateTime.UtcNow,
                    Price = price,
                };

                await _checkRepository.Create(newCheck);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Сheck> GetById(long Id)
        {
            try
            {
                if(Id <= 0)
                {
                    return new Сheck() { };
                }

                var chek = _checkRepository.GetAll().FirstOrDefault(ch => ch.Id == Id);

                if(chek == null) 
                { 
                    return new Сheck() { }; 
                }

                return chek;
            }
            catch(Exception ex)
            {
                return new Сheck() { };
            }
        }
        public async Task<List<Сheck>> GetByProductId(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new() { new Сheck() { } };
                }
                var chek = _checkRepository.GetAll();
                

                if (chek == null)
                {
                    return new() { new Сheck() { } };
                }

                return chek.ToList();
            }
            catch (Exception ex)
            {
                return new() { new Сheck() { } };
            }
        }
        public async Task<List<Сheck>> GetByWorkerId(long Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new() { new Сheck() { } };
                }
                var chek =  _checkRepository.GetAll().Where(ch => ch.Worker.Id == Id);

                if (chek == null)
                {
                    return new() { new Сheck() { } };
                }

                return chek.ToList();
            }
            catch (Exception ex)
            {
                return new() { new Сheck() { } };
            }
        }
        public async Task<bool> Edit(Сheck check)
        {
            try
            {
                if(check == null)
                {
                    return false;
                }
                await _checkRepository.Edit(check);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}

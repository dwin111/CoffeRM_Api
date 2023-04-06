using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.Enums;
using CoffeCRMBeck.Model.ViewModel;
using CoffeCRMBeck.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace CoffeCRMBeck.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : Controller
    {
        private readonly CheckService _checkService;
        private readonly ProductService _productService;
        private readonly ProductCatalogService _productCatalogService;

        private readonly WorkerService _workerService;
        private AppDbContext _db;
        //private int AccountId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value); //Other worker id
        public CheckController(CheckService checkService, ProductService productService, WorkerService workerService, ProductCatalogService productCatalogService , AppDbContext db)
        {
            _checkService = checkService;
            _productService = productService;
            _workerService = workerService;
            _productCatalogService = productCatalogService;
            _db = db;
        }


        [HttpGet("Test")]
        public IActionResult Test()
        {
            var check = _db.Сhecks.Include(p => p.Products).Include(p => p.Worker);
            return Ok(check);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var model = _db.Сhecks.Include(p => p.Products).Include(x => x.Worker).ToList();
            var allCheck = _checkService.GetAll();
            //Console.WriteLine(allCheck);
            if (allCheck == null)
            {
                return NotFound();
            }
            foreach (var check in allCheck)
            {
                check.Price = 0;
                foreach (var product in check.Products)
                {
                    check.Price += (float)product.TotalPrice;
                }
            }
            return Ok(allCheck);
        }


        [HttpPost("NewCheck")]
        public async Task<IActionResult> CreateCheck(CheckViewModel checkViewModel)
        {
            try
            {
                await _checkService.Create(checkViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPost("NewCheck/{checkId}/{workerId}")]
        public async Task<IActionResult> CreateCheckFromId(long? checkId, long workerId, Dictionary<long,long> ProductsIdAnda)
        {
            try
            {
                var worker = await _workerService.GetById(workerId);
                List<Product> products = new();
                List<ProductCatalog> productCatalogs = new();
                if (ProductsIdAnda.Count <= 0 || worker == null)
                {
                    return NotFound();
                }
                if (checkId == null || checkId == 0)
                {
                    foreach (var product in ProductsIdAnda)
                    {
                        var productCatalog = await _productCatalogService.GetById(product.Key);
                        productCatalogs.Add(productCatalog);

                        products.Add(new Product()
                        {
                            Id = 0,
                            Name = productCatalog.Name,
                            Price = productCatalog.Price,
                            Amount = product.Value,
                            Date = DateTime.UtcNow,
                            TotalPrice = productCatalog.Price * product.Value,
                            Type = productCatalog.Type,

                        });
                    }
                    var check = new CheckViewModel()
                    {
                        Products = products,
                        Worker = worker
                    };
                    await _checkService.Create(check);
                    return Ok();
                }
                else
                {
                    var check = await _checkService.GetById((long)checkId);
                    var productsInCheck = check.Products;
                    foreach (var productId in ProductsIdAnda)
                    {
                        var productCatalog = await _productCatalogService.GetById(productId.Key);
                        productCatalogs.Add(productCatalog);

                        bool isProductInList = false;
                        for (int i = 0; i < productsInCheck.Count; i++)
                        {
                            if (productsInCheck[i].Name == productCatalog.Name && !isProductInList)
                            {
                                productsInCheck[i].Amount += productId.Value;
                                productsInCheck[i].TotalPrice += productsInCheck[i].Price * productId.Value;
                                isProductInList = true;
                            }
                            else if (i == productsInCheck.Count - 1 && !isProductInList)
                            {
                                var product = new Product()
                                {
                                    Id = 0,
                                    Name = productCatalog.Name,
                                    Price = productCatalog.Price,
                                    Amount = productId.Value,
                                    Date = DateTime.UtcNow,
                                    TotalPrice = productCatalog.Price * productId.Value,
                                    Type = productCatalog.Type,

                                };
                                check.Products.Add(product);
                                isProductInList = true;
                            }
                        }
                    }
                    if (!_checkService.Edit(check).Result)
                    {
                        return NotFound();
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }



        
        // if checkId == 0 then dont check
        [HttpGet("NewCheckWithId/{checkId}/{workerId}/{productId}/{amoutProduct}")]
        public async Task<IActionResult> CreateCheckWithId(long? checkId, long workerId, int productId, long amoutProduct)
        {
            try
            {
                var productCatalog = await _productCatalogService.GetById(productId);
                var worker = await _workerService.GetById(workerId);
                Product product = null;
                if (productCatalog == null || worker == null)
                {
                    return NotFound();
                }
                if (checkId == null || checkId == 0)
                {
                    product = new Product()
                    { 
                        Id = 0,
                        Name = productCatalog.Name,
                        Price= productCatalog.Price,
                        Amount = amoutProduct,
                        Date = DateTime.UtcNow,
                        TotalPrice = productCatalog.Price,
                        Type = productCatalog.Type,

                    };
                    var check = new CheckViewModel()
                    {
                        Products = new() { product },
                        Worker = worker
                    };
                    await _checkService.Create(check);
                    return Ok();
                }
                else
                {
                    var check = await _checkService.GetById((long)checkId);
                    var products = check.Products;
                    foreach (var item in products)
                    {
                        product = await _productService.GetByNameAndById(item.Id, productCatalog.Name);
                    }
                    if(product == null || product.Name == null)
                    {
                        product = new Product()
                        {
                            Id = 0,
                            Name = productCatalog.Name,
                            Price = productCatalog.Price,
                            Amount = amoutProduct,
                            Date = DateTime.UtcNow,
                            TotalPrice = productCatalog.Price,
                            Type = productCatalog.Type,

                        };
                        check.Products.Add(product);
                    }
                    else
                    {
                        product.Amount += amoutProduct;
                        product.TotalPrice += product.Price;
                    }

                    if (!_checkService.Edit(check).Result)
                    {
                        return NotFound();
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

        //[HttpGet("DeleteById/{checkId}")]
        //public async Task<IActionResult> DeleteById(long checkId)
        //{
        //    try
        //    {

        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

    }
}

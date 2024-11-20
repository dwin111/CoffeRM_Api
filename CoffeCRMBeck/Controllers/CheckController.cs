using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;
using CoffeCRMBeck.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : Controller
    {
        private readonly CheckService _checkService;
        private readonly ProductService _orderService;
        private readonly ProductCatalogService _menuService;

        private readonly WorkerService _staffService;
        private AppDbContext _db;
        //private int AccountId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value); //Other staff id
        public CheckController(CheckService checkService, ProductService orderService, WorkerService staffService, ProductCatalogService menuService, AppDbContext db)
        {
            _checkService = checkService;
            _orderService = orderService;
            _staffService = staffService;
            _menuService = menuService;
            _db = db;
        }


        [HttpGet("Test")]
        public IActionResult Test()
        {
            var check = _db.Bill.Include(p => p.Orders).Include(p => p.Staff);
            return Ok(check);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            //var model = _db.Сhecks.Include(p => p.orders).Include(x => x.staff).ToList();
            var allCheck = _checkService.GetAll();
            //Console.WriteLine(allCheck);
            if (allCheck == null)
            {
                return NotFound();
            }
            foreach (var check in allCheck)
            {
                check.Price = 0;
                foreach (var order in check.Orders)
                {
                    check.Price += (float)order.TotalPrice;
                }
            }
            return Ok(allCheck);
        }


        [HttpPost("NewCheck")]
        public async Task<IActionResult> CreateCheckAsync(CheckViewModel checkViewModel)
        {
            try
            {
                await _checkService.CreateAsync(checkViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPost("NewCheck/{checkId}/{staffId}")]
        public async Task<IActionResult> CreateCheckFromIdAsync(long? checkId, long staffId, Dictionary<long, long> ordersIdAnda)
        {
            try
            {
                var staff = await _staffService.GetByIdAsync(staffId);
                List<Order> orders = new();
                List<Menu> menus = new();
                if (ordersIdAnda.Count <= 0 || staff == null)
                {
                    return NotFound();
                }
                if (checkId == null || checkId == 0)
                {
                    foreach (var order in ordersIdAnda)
                    {
                        var menu = await _menuService.GetByIdAsync(order.Key);
                        menus.Add(menu);

                        orders.Add(new Order()
                        {
                            Id = 0,
                            Name = menu.Name,
                            Price = menu.Price,
                            Amount = order.Value,
                            Date = DateTime.UtcNow,
                            TotalPrice = menu.Price * order.Value,
                            Type = menu.Type,

                        });
                    }
                    var check = new CheckViewModel()
                    {
                        Orders = orders,
                        Staff = staff
                    };
                    await _checkService.CreateAsync(check);
                    return Ok();
                }
                else
                {
                    var check = await _checkService.GetByIdAsync((long)checkId);
                    var ordersInCheck = check.Orders;
                    foreach (var orderId in ordersIdAnda)
                    {
                        var menu = await _menuService.GetByIdAsync(orderId.Key);
                        menus.Add(menu);

                        bool isorderInList = false;
                        for (int i = 0; i < ordersInCheck.Count; i++)
                        {
                            if (ordersInCheck[i].Name == menu.Name && !isorderInList)
                            {
                                ordersInCheck[i].Amount += orderId.Value;
                                ordersInCheck[i].TotalPrice += ordersInCheck[i].Price * orderId.Value;
                                isorderInList = true;
                            }
                            else if (i == ordersInCheck.Count - 1 && !isorderInList)
                            {
                                var order = new Order()
                                {
                                    Id = 0,
                                    Name = menu.Name,
                                    Price = menu.Price,
                                    Amount = orderId.Value,
                                    Date = DateTime.UtcNow,
                                    TotalPrice = menu.Price * orderId.Value,
                                    Type = menu.Type,

                                };
                                check.Orders.Add(order);
                                isorderInList = true;
                            }
                        }
                    }
                    if (!(await _checkService.EditAsync(check)))
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
        [HttpGet("NewCheckWithId/{checkId}/{staffId}/{orderId}/{amoutorder}")]
        public async Task<IActionResult> CreateCheckWithIdAsync(long? checkId, long staffId, int orderId, long amoutorder)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(orderId);
                var staff = await _staffService.GetByIdAsync(staffId);
                Order order = null;
                if (menu == null || staff == null)
                {
                    return NotFound();
                }
                if (checkId == null || checkId == 0)
                {
                    order = new Order()
                    {
                        Id = 0,
                        Name = menu.Name,
                        Price = menu.Price,
                        Amount = amoutorder,
                        Date = DateTime.UtcNow,
                        TotalPrice = menu.Price,
                        Type = menu.Type,

                    };
                    var check = new CheckViewModel()
                    {
                        Orders = new() { order },
                        Staff = staff
                    };
                    await _checkService.CreateAsync(check);
                    return Ok();
                }
                else
                {
                    var check = await _checkService.GetByIdAsync((long)checkId);
                    var orders = check.Orders;
                    foreach (var item in orders)
                    {
                        order = await _orderService.GetByNameAndByIdAsync(item.Id, menu.Name);
                    }
                    if (order == null || order.Name == null)
                    {
                        order = new Order()
                        {
                            Id = 0,
                            Name = menu.Name,
                            Price = menu.Price,
                            Amount = amoutorder,
                            Date = DateTime.UtcNow,
                            TotalPrice = menu.Price,
                            Type = menu.Type,

                        };
                        check.Orders.Add(order);
                    }
                    else
                    {
                        order.Amount += amoutorder;
                        order.TotalPrice += order.Price;
                    }

                    if (!(await _checkService.EditAsync(check)))
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

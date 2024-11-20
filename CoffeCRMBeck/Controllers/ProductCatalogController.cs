using CoffeCRMBeck.Model;
using CoffeCRMBeck.Model.ViewModel;
using CoffeCRMBeck.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeCRMBeck.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCatalogController : Controller
    {
        private readonly ProductCatalogService _productCatalogService;
        //private int AccountId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ProductCatalogController(ProductCatalogService productCatalogService)
        {
            _productCatalogService = productCatalogService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var allCheck = await _productCatalogService.GetAllAsync();
            if (allCheck == null)
            {
                return NotFound();
            }
            return Ok(allCheck);
        }

        [HttpPost("NewProduct")]
        public async Task<IActionResult> CreateAsync(ProductCatalogViewModel productViewModel)
        {
            try
            {
                if (!await _productCatalogService.Create(productViewModel))
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPost("FullEdit")]
        public async Task<IActionResult> FullEditAsync(Menu productCatalogModel)
        {
            try
            {
                if (!await _productCatalogService.FullEditAsync(productCatalogModel))
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Forbid(ex.Message);
            }
        }
    }
}


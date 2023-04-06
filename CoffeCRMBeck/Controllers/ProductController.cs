using CoffeCRMBeck.Model.ViewModel;
using CoffeCRMBeck.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeCRMBeck.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        //private int AccountId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allCheck = await _productService.GetAll();
            if (allCheck == null)
            {
                return NotFound();
            }
            return Ok(allCheck);
        }

        [HttpPost("NewProduct")]
        public async Task<IActionResult> CreateCheck(ProductViewModel productViewModel)
        {
            try
            {
                await _productService.Create(productViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

    }
}

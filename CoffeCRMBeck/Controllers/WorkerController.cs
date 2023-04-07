using CoffeCRMBeck.Model.ViewModel;
using CoffeCRMBeck.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeCRMBeck.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : Controller
    {
        private readonly WorkerService _workerService;
        //private int AccountId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allCheck = await _workerService.GetAll();
            if (allCheck == null)
            {
                return NotFound();
            }
            return Ok(allCheck);
        }

        [HttpPost("NewWorker")]
        public async Task<IActionResult> CreateCheck(WorkerViewModel workerViewModel)
        {
            try
            {
                await _workerService.Create(workerViewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }


    }
}

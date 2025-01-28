using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrDetailsController : ControllerBase
    {
        private readonly IHrDetails _hrDetailsRepo;

        public HrDetailsController(IHrDetails hrDetailsRepo)
        {
            _hrDetailsRepo = hrDetailsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HrDetails>>> GetHrDetails()
        {
            try
            {
                return Ok(await _hrDetailsRepo.GetDetailsRepo(1));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {
                    errorMessage = "Internal server error --->",
                    errorDetails = ex.Message,ex.InnerException?.Message});
            }
        }
        [HttpPut]
        public async Task<ActionResult<HrDetails>> EditHrDetails(HrDetails hrDetails)
        {
            try
            {
                var updated = await _hrDetailsRepo.EditDetailsRepo(1,hrDetails);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }
            catch (Exception ex)
            {   
                return StatusCode(500, new {
                    errorMessage = "Internal server error --->",
                    errorDetails = ex.Message,ex.InnerException?.Message});
            }
        }   
    }
}

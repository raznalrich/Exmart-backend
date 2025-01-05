using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAdminMember([FromBody] int userId)
        {
            var isAdded = _adminRepository.AddAdminMember(userId);
            if (isAdded != null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest("Something went wrong");
        }
        [HttpGet("Check")]
        public async Task<IActionResult> CheckAdminExisted([FromBody] int userId)
        {
            var isExisted = _adminRepository.CheckAdminMembers(userId);
            if (isExisted != null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("remove/{userId}")]
        public async Task<IActionResult> RemoveAdminMember([FromBody] int userId)
        {
            var isDeleted = _adminRepository.DeleteAdminMember(userId);
            if (isDeleted != null)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            return BadRequest("Something went wrong");

        }
    }
}

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
        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddAdminMember(int userId)
        {
            var isAdded = await _adminRepository.AddAdminMember(userId); // Await the repository method

            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created, "User added as admin successfully.");
            }

            return BadRequest("User could not be added as admin. Either the user does not exist or is already an admin.");
        }
        [HttpGet("Check/{userId}")]
        public async Task<IActionResult> CheckAdminExisted(int userId)
        {
            var result = await _adminRepository.CheckAdminMembers(userId);
            if (!result)
            {
                return NotFound(new { message = "admin not founded" });
            }

            return Ok(new { message = "admin existed" });
        }
        [HttpDelete("remove/{userId}")]
        public async Task<IActionResult> RemoveAdminMember(int userId)
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

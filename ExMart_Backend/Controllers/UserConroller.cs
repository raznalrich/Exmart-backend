using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConroller : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User users)
        {
            var isAdded = await _userRepository.AddUser(users);
            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest("Something went wrong");
        }
    }
}

using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User users)
        {
            var IsAdded = await _userRepository.AddUser(users);
            if (IsAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest("Something went wrong");
        }
    }
}

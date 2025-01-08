using AutoMapper;
using ExMart_Backend.Data;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IConfigRepository _configRepository;
        public ConfigController(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }
   
        [HttpGet("GetColorById")]
        public async Task<IActionResult> GetColorById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be greater than 0.");
            }

            var color = await _configRepository.GetColorById(id);
            if (color == null)
            {
                return NotFound($"No color found with ID {id}.");
            }

            return Ok(color);
        }

        [HttpGet("GetSizeById")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be greater than 0.");
            }

            var size = await _configRepository.GetSizeById(id);
            if (size == null)
            {
                return NotFound($"No size found with ID {id}.");
            }

            return Ok(size);
        }

    }
}

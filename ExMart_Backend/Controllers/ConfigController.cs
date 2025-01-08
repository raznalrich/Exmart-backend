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
        private readonly IConfigRepository _configRepository;

        public ConfigController(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        // Existing Endpoint: Get Color by ID
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

        // Existing Endpoint: Get Size by ID
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

        // New Endpoint: Get All Colors
        [HttpGet("GetAllColors")]
        public async Task<IActionResult> GetAllColors()
        {
            var colors = await _configRepository.GetAllColors();
            if (colors == null || !colors.Any())
            {
                return NotFound("No colors found.");
            }

            return Ok(colors);
        }

        // New Endpoint: Get All Sizes
        [HttpGet("GetAllSizes")]
        public async Task<IActionResult> GetAllSizes()
        {
            var sizes = await _configRepository.GetAllSizes();
            if (sizes == null || !sizes.Any())
            {
                return NotFound("No sizes found.");
            }

            return Ok(sizes);
        }
    }
}

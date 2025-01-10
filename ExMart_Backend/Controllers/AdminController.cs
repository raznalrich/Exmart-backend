using System;
using System.Threading.Tasks;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        }

        [HttpPost("add/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAdminMember([Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { Message = "Invalid user ID. User ID must be greater than 0." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid request", Errors = ModelState });
                }

                var isAdded = await _adminRepository.AddAdminMember(userId);

                if (isAdded)
                {
                    return StatusCode(
                        StatusCodes.Status201Created,
                        new { Message = "User added as admin successfully", UserId = userId }
                    );
                }

                return BadRequest(new
                {
                    Message = "User could not be added as admin. Either the user does not exist or is already an admin.",
                    UserId = userId
                });
            }
            catch (InvalidOperationException ex)
            {
                // Log the specific error here
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new { Message = ex.Message }
                );
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while processing your request" }
                );
            }
        }

        [HttpGet("Check/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckAdminExisted([Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { Message = "Invalid user ID. User ID must be greater than 0." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid request", Errors = ModelState });
                }

                var result = await _adminRepository.CheckAdminMembers(userId);

                if (!result)
                {
                    return NotFound(new
                    {
                        Message = "Admin not found",
                        UserId = userId
                    });
                }

                return Ok(new
                {
                    Message = "Admin exists",
                    UserId = userId,
                    IsAdmin = true
                });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while checking admin status" }
                );
            }
        }

        [HttpDelete("remove/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdminMember([Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0")] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { Message = "Invalid user ID. User ID must be greater than 0." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid request", Errors = ModelState });
                }

                var isDeleted = await _adminRepository.DeleteAdminMember(userId);

                if (isDeleted)
                {
                    return Ok(new
                    {
                        Message = "Admin member removed successfully",
                        UserId = userId
                    });
                }

                return NotFound(new
                {
                    Message = "Admin member not found",
                    UserId = userId
                });
            }
            catch (InvalidOperationException ex)
            {
                // Log the specific error here
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new { Message = ex.Message }
                );
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while removing admin member" }
                );
            }
        }
    }
}
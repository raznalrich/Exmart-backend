using ExMart_Backend.DTO;
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

        [HttpPost]
        [Route("addAddress")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressDTO addAddressDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userRepository.AddAddress(addAddressDTO);
                return Ok(new { Message = "Address added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the address.", Details = ex.Message });
            }
        }

        [HttpGet("getAddress/{userId}")]
        public async Task<IActionResult> GetUserAddresses(int userId)
        {
            var userAddresses = await _userRepository.GetAddressByUserId(userId);

            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { Message = "No addresses found for the given user." });
            }

            var AddressDTOs = userAddresses.Select(address => new AddressDTO
            {
                Id = address.Id,
                IsPrimary = address.IsPrimary,
                AddressLine = address.AddressLine,
                City = address.City,
                State = address.State,
                District = address.District,
                ZipCode = address.ZipCode,
                AddressTypeName = address.AddressType?.AddressTypeName
            }).ToList();

            return Ok(AddressDTOs);
        }

        [HttpPut("EditAddress")]
        public async Task<IActionResult> EditAddress(int id, [FromBody] AddAddressDTO editAddressDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Address ID.");
            }

            if (editAddressDTO == null)
            {
                return BadRequest("Address data cannot be null.");
            }

            var isUpdated = await _userRepository.EditAddressById(id, editAddressDTO);

            if (!isUpdated)
            {
                return NotFound($"No address found with ID {id}.");
            }

            return Ok("Address updated successfully.");
        }


    }
}

using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ExMart_Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //[HttpPost]
        //public async Task<IActionResult> AddNewUser([FromBody] User users)
        //{
        //    var IsAdded = await _userRepository.AddUser(users);
        //    if (IsAdded)
        //    {
        //        return StatusCode(StatusCodes.Status201Created);
        //    }
        //    return BadRequest("Something went wrong");
        //}

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAddress>>> GetUserAddress()
        {
            try
            {
                var address = await _userRepository.GetUserAddress();
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving categories: {ex.Message}");
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
        [HttpGet("getAddressById/{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            UserAddress address = await _userRepository.GetAddressById(id);
            if (address == null)
                return NotFound("Address not found.");

            return Ok(address);
        }
        [HttpPut("EditAddress/{id}")]
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

        [HttpDelete("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddressById(int id)
        {
            var result = await _userRepository.DeleteAddressById(id);

            if (!result)
            {
                return NotFound(new { message = "Address not found or already deleted." });
            }

            return Ok(new { message = "Address deleted successfully." });
        }

        //[HttpGet("CheckUserExisted/{userId}")]
        //public async Task<IActionResult> CheckUserExisted(int userId)
        //{
        //    var result = await _userRepository.IsUserExisted(userId);
        //    if (!result)
        //    {
        //        return NotFound(new { message = "user not founded" });
        //    }

        //    return Ok(new { message = "user existed" });
        //}

        //[HttpGet("ReturnIdfromemail/{email}")]
        //public async Task<IActionResult> ReturnIdfromEmail(string email)
        //{
        //    int? userid = await _userRepository.ReturnIdbyEmail(email);
        //    if (userid == 0)
        //    {
        //        return BadRequest(null);
        //    }
        //    return Ok(userid);

        //}

        //[HttpGet("ReturnEmailFromId/{id}")]
        //public async Task<IActionResult> ReturnEmailFromId(int id)
        //{
        //    string? userEmail = await _userRepository.ReturnEmailById(id);
        //    if (userEmail == null)
        //    {
        //        return BadRequest(null);
        //    }
        //    return Ok(userEmail);

        //}


        [HttpPost]
        public async Task<IActionResult> AddNewUser([FromBody] User users)
        {
            if (users == null)
            {
                return BadRequest(new { message = "User data cannot be null." });
            }

            try
            {
                var isAdded = await _userRepository.AddUser(users);
                if (isAdded)
                {
                    return StatusCode(StatusCodes.Status201Created, new { message = "User added successfully." });
                }
                return BadRequest(new { message = "An error occurred while adding the user." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while adding the user: {ex.Message}"
                });
            }
        }

        [HttpGet("CheckUserExisted/{userId}")]
        public async Task<IActionResult> CheckUserExisted(int userId)
        {
            try
            {
                var result = await _userRepository.IsUserExisted(userId);
                if (!result)
                {
                    return NotFound(new { message = "User not found." });
                }
                return Ok(new { message = "User exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while checking user existence: {ex.Message}"
                });
            }
        }

        [HttpGet("ReturnIdfromemail/{email}")]
        public async Task<IActionResult> ReturnIdfromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "Email cannot be empty." });
            }

            try
            {
                int? userId = await _userRepository.ReturnIdbyEmail(email);
                if (userId == null || userId == 0)
                {
                    return NotFound(new { message = "User not found for the provided email." });
                }
                return Ok(new { userId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while retrieving user ID: {ex.Message}"
                });
            }
        }

        [HttpGet("ReturnEmailFromId/{id}")]
        public async Task<IActionResult> ReturnEmailFromId(int id)
        {
            try
            {
                string? userEmail = await _userRepository.ReturnEmailById(id);
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    return NotFound(new { message = "Email not found for the provided user ID." });
                }
                return Ok(new { userEmail });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while retrieving user email: {ex.Message}"
                });
            }
        }
    }
}

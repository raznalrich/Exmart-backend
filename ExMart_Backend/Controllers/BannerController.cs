using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerController(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddBanner([FromBody] Banner banner)
        {
            if (banner == null)
            {
                return BadRequest("Banner data is null.");
            }

            try
            {
                var createdBanner = await _bannerRepository.AddBannerAsync(banner);
                return CreatedAtAction(nameof(AddBanner), new { id = createdBanner.BannerId }, createdBanner);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the banner: " + ex.Message);
            }
        }

        [HttpGet("ByProduct/{productId}")]
        public async Task<IActionResult> GetBannersByProductId(int productId)
        {
            try
            {
                var banners = await _bannerRepository.GetBannersByProductIdAsync(productId);

                if (banners == null || !banners.Any())
                {
                    return NotFound($"No banners found for ProductId: {productId}");
                }

                return Ok(banners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching banners: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(int id, [FromBody] Banner updatedBanner)
        {
            if (updatedBanner == null || id != updatedBanner.BannerId)
            {
                return BadRequest("Invalid Banner data or mismatched ID.");
            }

            try
            {
                var existingBanner = await _bannerRepository.GetBannerByIdAsync(id);
                if (existingBanner == null)
                {
                    return NotFound(new { message = $"Banner with ID {id} not found." });
                }

                // Ensure the ProductId exists in the database
                var productExists = await _bannerRepository.ProductExistsAsync(updatedBanner.ProductId);
                if (!productExists)
                {
                    return BadRequest(new { message = "Invalid ProductId. The product does not exist." });
                }

                // Update the banner properties
                existingBanner.ImageUrl = updatedBanner.ImageUrl;
                existingBanner.ProductName = updatedBanner.ProductName;
                existingBanner.ProductId = updatedBanner.ProductId;

                await _bannerRepository.UpdateBannerAsync(existingBanner);

                return Ok(new { message = $"Banner with ID {id} has been updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating the banner: {ex.Message}" });
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            try
            {
                var banners = await _bannerRepository.GetAllBannersAsync();

                if (banners == null || !banners.Any())
                {
                    return NotFound("No banners found.");
                }

                return Ok(banners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching all banners: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            try
            {
                var banner = await _bannerRepository.GetBannerByIdAsync(id);
                if (banner == null)
                {
                    return NotFound($"Banner with ID {id} not found.");
                }

                await _bannerRepository.DeleteBannerAsync(id);
                return Ok($"Banner with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the banner: " + ex.Message);
            }
        }

        [HttpGet("Detailed")]
        public async Task<IActionResult> GetDetailedBanners()
        {
            try
            {
                var bannerDetails = await _bannerRepository.GetAllBannerDetailsAsync();

                if (bannerDetails == null || !bannerDetails.Any())
                {
                    return NotFound("No banners found.");
                }

                return Ok(bannerDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching banner details: " + ex.Message);
            }
        }



    }
}
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




    }
}

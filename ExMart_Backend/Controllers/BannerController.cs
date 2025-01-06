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



    }
}

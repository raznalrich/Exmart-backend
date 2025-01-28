using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IBannerRepository
    {
        Task<Banner> AddBannerAsync(Banner banner);
        Task<IEnumerable<Banner>> GetBannersByProductIdAsync(int productId);

        Task<IEnumerable<Banner>> GetAllBannersAsync();

        Task<Banner> GetBannerByIdAsync(int id);
        Task DeleteBannerAsync(int id);

        Task UpdateBannerAsync(Banner banner);
        Task<bool> ProductExistsAsync(int productId);

        Task<IEnumerable<BannerDTO>> GetAllBannerDetailsAsync();



    }
}
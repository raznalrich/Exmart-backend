using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExMart_Backend.Services.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ApplicationDBContext _db;

        public BannerRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Banner> AddBannerAsync(Banner banner)
        {
            // Ensure the ProductId exists in the database
            var productExists = await _db.Products.AnyAsync(p => p.Id == banner.ProductId);
            if (!productExists)
            {
                throw new ArgumentException("Invalid ProductId. The product does not exist.");
            }

            // Add the banner to the database
            await _db.Banners.AddAsync(banner);
            await _db.SaveChangesAsync();

            return banner;
        }

        public async Task<IEnumerable<Banner>> GetBannersByProductIdAsync(int productId)
        {
            // Fetch banners with the given ProductId
            return await _db.Banners
                            .Where(b => b.ProductId == productId)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Banner>> GetAllBannersAsync()
        {
            return await _db.Banners.ToListAsync();
        }

        public async Task UpdateBannerAsync(Banner banner)
        {
            _db.Banners.Update(banner);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _db.Products.AnyAsync(p => p.Id == productId);
        }

        public async Task<Banner> GetBannerByIdAsync(int id)
        {
            return await _db.Banners.FindAsync(id);
        }

        public async Task DeleteBannerAsync(int id)
        {
            var banner = await GetBannerByIdAsync(id);
            if (banner == null)
            {
                throw new ArgumentException($"Banner with ID {id} not found.");
            }

            _db.Banners.Remove(banner);
            await _db.SaveChangesAsync();
        }

        // **New Method: GetAllBannerDetailsAsync**
        public async Task<IEnumerable<BannerDTO>> GetAllBannerDetailsAsync()
        {
            var bannerDetails = await (from banner in _db.Banners
                                       join product in _db.Products on banner.ProductId equals product.Id
                                       join category in _db.addToCategories on product.CategoryId equals category.Id
                                       select new BannerDTO
                                       {
                                           BannerId = banner.BannerId,
                                           ImageUrl = banner.ImageUrl,
                                           ProductId = product.Id,
                                           ProductImage = product.PrimaryImageUrl,
                                           CategoryName = category.CategoryName,
                                           ProductName = product.Name,
                                           ProductPrice = product.Price.ToString("C") // Formats price as currency
                                       }).ToListAsync();

            return bannerDetails;
        }
    }
}
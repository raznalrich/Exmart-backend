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
            _db = db ?? throw new ArgumentNullException(nameof(db), "Database context cannot be null.");
        }

        public async Task<Banner> AddBannerAsync(Banner banner)
        {
            if (banner == null)
            {
                throw new ArgumentNullException(nameof(banner), "Banner object cannot be null.");
            }

            var productExists = await _db.Products.AnyAsync(p => p.Id == banner.ProductId);
            if (!productExists)
            {
                throw new ArgumentException("Invalid ProductId. The product does not exist.");
            }

            try
            {
                await _db.Banners.AddAsync(banner);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while adding the banner to the database.", ex);
            }

            return banner;
        }

        public async Task<IEnumerable<Banner>> GetBannersByProductIdAsync(int productId)
        {
            try
            {
                return await _db.Banners.Where(b => b.ProductId == productId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving banners by ProductId.", ex);
            }
        }

        public async Task<IEnumerable<Banner>> GetAllBannersAsync()
        {
            try
            {
                return await _db.Banners.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all banners.", ex);
            }
        }

        public async Task UpdateBannerAsync(Banner banner)
        {
            if (banner == null)
            {
                throw new ArgumentNullException(nameof(banner), "Banner object cannot be null.");
            }

            try
            {
                _db.Banners.Update(banner);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while updating the banner.", ex);
            }
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            try
            {
                return await _db.Products.AnyAsync(p => p.Id == productId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while checking if the product exists.", ex);
            }
        }

        public async Task<Banner> GetBannerByIdAsync(int id)
        {
            try
            {
                var banner = await _db.Banners.FindAsync(id);
                if (banner == null)
                {
                    throw new KeyNotFoundException($"Banner with ID {id} not found.");
                }
                return banner;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the banner by ID.", ex);
            }
        }

        public async Task DeleteBannerAsync(int id)
        {
            var banner = await GetBannerByIdAsync(id);
            if (banner == null)
            {
                throw new ArgumentException($"Banner with ID {id} not found.");
            }

            try
            {
                _db.Banners.Remove(banner);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the banner.", ex);
            }
        }

        public async Task<IEnumerable<BannerDTO>> GetAllBannerDetailsAsync()
        {
            try
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
                                               ProductPrice = product.Price.ToString("C")
                                           }).ToListAsync();

                return bannerDetails;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving banner details.", ex);
            }
        }
    }
}
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ExMart_Backend.Services.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDBContext _db;

        public AdminRepository(ApplicationDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db), "Database context cannot be null");
        }

        public async Task<bool> AddAdminMember(int userId)
        {
            try
            {
         
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

                if (_db.Users == null || _db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

          
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with ID {userId} does not exist");
                }

                var isExistingAdmin = await _db.AdminMembers
                    .AnyAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);

                if (isExistingAdmin)
                {
                    throw new InvalidOperationException($"User with ID {userId} is already an admin member");
                }

                var adminMember = new AdminMembers
                {
                    UserId = userId,
                    AddedDate = DateTime.UtcNow
                };

                await _db.AdminMembers.AddAsync(adminMember).ConfigureAwait(false);

      
                var savedChanges = await SaveChangesWithRetryAsync().ConfigureAwait(false);
                return savedChanges > 0;
            }
            catch (DbUpdateException ex)
            {
           
                throw new InvalidOperationException("Failed to add admin member due to database error", ex);
            }
            catch (Exception ex)
            {
      
                throw;
            }
        }

        public async Task<bool> CheckAdminMembers(int userId)
        {
            try
            {
        
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

        
                if (_db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

             
                return await _db.AdminMembers
                    .AnyAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
             
                throw;
            }
        }

        public async Task<bool> DeleteAdminMember(int userId)
        {
            try
            {
               
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

                if (_db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

           
                var adminMember = await _db.AdminMembers
                    .FirstOrDefaultAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);

                if (adminMember == null)
                {
                    throw new InvalidOperationException($"Admin member with user ID {userId} does not exist");
                }

            
                _db.AdminMembers.Remove(adminMember);

       
                var savedChanges = await SaveChangesWithRetryAsync().ConfigureAwait(false);
                return savedChanges > 0;
            }
            catch (DbUpdateException ex)
            {
           
                // Logger.LogError($"Database error while deleting admin member: {ex.Message}", ex);
                throw new InvalidOperationException("Failed to delete admin member due to database error", ex);
            }
            catch (Exception ex)
            {
             
                throw;
            }
        }

        private async Task<int> SaveChangesWithRetryAsync(int maxRetries = 3)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    return await _db.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (DbUpdateConcurrencyException) when (i < maxRetries - 1)
                {
            
                    await Task.Delay((int)Math.Pow(2, i) * 100).ConfigureAwait(false);
                    continue;
                }
            }
            throw new InvalidOperationException("Failed to save changes after multiple retries");
        }
    }
}
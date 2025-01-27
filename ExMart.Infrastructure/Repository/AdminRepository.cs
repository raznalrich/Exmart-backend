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
                // Validate input
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

                // Validate database context
                if (_db.Users == null || _db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

                // Check if the user exists
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with ID {userId} does not exist");
                }

                // Check if the user is already an admin
                var isExistingAdmin = await _db.AdminMembers
                    .AnyAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);

                if (isExistingAdmin)
                {
                    throw new InvalidOperationException($"User with ID {userId} is already an admin member");
                }

                // Create new admin member
                var adminMember = new AdminMembers
                {
                    UserId = userId,
                    AddedDate = DateTime.UtcNow
                };

                // Add to database
                await _db.AdminMembers.AddAsync(adminMember).ConfigureAwait(false);

                // Save changes and handle potential concurrency issues
                var savedChanges = await SaveChangesWithRetryAsync().ConfigureAwait(false);
                return savedChanges > 0;
            }
            catch (DbUpdateException ex)
            {
                // Log database update exception
                // Logger.LogError($"Database error while adding admin member: {ex.Message}", ex);
                throw new InvalidOperationException("Failed to add admin member due to database error", ex);
            }
            catch (Exception ex)
            {
                // Log general exception
                // Logger.LogError($"Error adding admin member: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<bool> CheckAdminMembers(int userId)
        {
            try
            {
                // Validate input
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

                // Validate database context
                if (_db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

                // Check admin status
                return await _db.AdminMembers
                    .AnyAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Log exception
                //Logger.LogError($"Error checking admin status: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<bool> DeleteAdminMember(int userId)
        {
            try
            {
                // Validate input
                if (userId <= 0)
                {
                    throw new ArgumentException("User ID must be greater than zero", nameof(userId));
                }

                // Validate database context
                if (_db.AdminMembers == null)
                {
                    throw new InvalidOperationException("Database context is not properly initialized");
                }

                // Find admin member
                var adminMember = await _db.AdminMembers
                    .FirstOrDefaultAsync(m => m.UserId == userId)
                    .ConfigureAwait(false);

                if (adminMember == null)
                {
                    throw new InvalidOperationException($"Admin member with user ID {userId} does not exist");
                }

                // Remove admin member
                _db.AdminMembers.Remove(adminMember);

                // Save changes and handle potential concurrency issues
                var savedChanges = await SaveChangesWithRetryAsync().ConfigureAwait(false);
                return savedChanges > 0;
            }
            catch (DbUpdateException ex)
            {
                // Log database update exception
                // Logger.LogError($"Database error while deleting admin member: {ex.Message}", ex);
                throw new InvalidOperationException("Failed to delete admin member due to database error", ex);
            }
            catch (Exception ex)
            {
                // Log general exception
                // Logger.LogError($"Error deleting admin member: {ex.Message}", ex);
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
                    // Wait before retrying (exponential backoff)
                    await Task.Delay((int)Math.Pow(2, i) * 100).ConfigureAwait(false);
                    continue;
                }
            }
            throw new InvalidOperationException("Failed to save changes after multiple retries");
        }
    }
}
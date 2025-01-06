using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class AdminRepository:IAdminRepository
    {
        public ApplicationDBContext _db;
        public AdminRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAdminMember(int userId)
        {
            // Check if the user exists
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            // Check if the user is already an admin
            if (await _db.AdminMembers.AnyAsync(m => m.UserId == userId))
            {
                return false;
            }

            // Add the user to the AdminMembers table
            var adminMember = new AdminMembers
            {
                UserId = userId,
                AddedDate = DateTime.UtcNow
            };

            _db.AdminMembers.Add(adminMember);
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CheckAdminMembers(int userId)
        {
            if (await _db.AdminMembers.AnyAsync(m => m.UserId == userId))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAdminMember(int userId)
        {
            var adminMember = await _db.AdminMembers.FirstOrDefaultAsync(m => m.UserId == userId);
            if (adminMember == null)
            {
                return false;
            }
            _db.AdminMembers.Remove(adminMember);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

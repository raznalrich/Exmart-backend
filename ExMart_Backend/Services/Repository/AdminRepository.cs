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
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            if(await _db.AdminMembers.AnyAsync(m => m.UserId == userId))
            {
                return false;
            }
            var adminMember = new AdminMembers
            {
                UserId = userId
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

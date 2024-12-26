using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;

namespace ExMart_Backend.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDBContext _db;
        public UserRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                int Id = _db.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

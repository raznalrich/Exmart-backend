using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
    }
}

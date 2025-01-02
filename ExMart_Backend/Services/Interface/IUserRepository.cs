using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task AddAddress(AddAddressDTO addAddressDTO);
        Task<List<UserAddress>> GetAddressByUserId(int userId);

    }
}

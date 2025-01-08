using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);

        Task<bool> IsUserExisted(int userId);
        Task AddAddress(AddAddressDTO addAddressDTO);
        Task<List<UserAddress>> GetAddressByUserId(int userId);
        Task<bool> EditAddressById(int id, AddAddressDTO editAddressDTO);
        Task<bool> DeleteAddressById(int id);
        Task<int?> ReturnIdbyEmail(string email);
        Task<string?> ReturnEmailById(int id);
    }
}

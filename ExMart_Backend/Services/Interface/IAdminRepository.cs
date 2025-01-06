using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IAdminRepository
    {
        Task<bool> AddAdminMember(int userId);

        Task<bool> CheckAdminMembers(int userId);
        Task<bool> DeleteAdminMember(int userId);
    }
}

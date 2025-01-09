using ExMart_Backend.DTO;

namespace ExMart_Backend.Services.Interface
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginReq);
    }
}

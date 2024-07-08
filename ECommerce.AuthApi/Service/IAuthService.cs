using ECommerce.AuthApi.Models;
using ECommerce.AuthApi.Models.DTO;

namespace ECommerce.AuthApi.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginDTO loginDto);
        
    }
}

using Identity_Server.DTOs;
using Identity_Server.Helpers;
using System.Threading.Tasks;

namespace Identity_Server.Services.Security
{
    public interface ISecurityService
    {
        Task<Response<ResgisterResponseDto>> RegisterAsync(RegisterDto dto);
        Task<Response<LoginResponseDto>> Login(string email, string password);
    }
}
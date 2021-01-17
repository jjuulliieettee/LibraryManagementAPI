using System.Threading.Tasks;
using LibraryManagementAPI.Core.Dtos;

namespace LibraryManagementAPI.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(string email, string password);
    }
}

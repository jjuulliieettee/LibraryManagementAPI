using System.Threading.Tasks;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto input)
        {
            return Ok(await _authService.Login(input.Email, input.Password));
        }
    }
}

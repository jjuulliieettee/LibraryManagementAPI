using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Configs;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IOptions<ApiOptions> _options;
        private readonly AuthConfigsManager _authConfigsManager;
        public AuthService(IUserService userService, IOptions<ApiOptions> options, AuthConfigsManager authConfigsManager)
        {
            _userService = userService;
            _authConfigsManager = authConfigsManager;
            _options = options;
        }

        public async Task<LoginResponseDto> Login(string email, string password)
        {
            ClaimsIdentity identity = await GetIdentity(email, password);

            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_options.Value.Lifetime)),
                    signingCredentials: new SigningCredentials(_authConfigsManager.GetSymmetricSecurityKey(_options.Value.Key), 
                        SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginResponseDto
            {
                AccessToken = encodedJwt,
                Email = identity.Name
            };
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await _userService.GetByEmailAsync(email);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }
            throw new AuthException();
        }
    }
}

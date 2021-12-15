using Identity_Server.Controllers.Helpers;
using Identity_Server.Data;
using Identity_Server.Data.Security;
using Identity_Server.DTOs;
using Identity_Server.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Server.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public SecurityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<Response<ResgisterResponseDto>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is not null)
                    return new Response<ResgisterResponseDto>("email is already existed!");

                ApplicationUser newUser = new()
                {
                    Email = dto.Email,
                    PhoneNumber = dto.Phone,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.CreateAsync(newUser, dto.Password);

                return new Response<ResgisterResponseDto>(new ResgisterResponseDto { Token = await GenerateBearerTokenForAUserAsync(newUser) });
            }
            catch (Exception)
            {
                return new Response<ResgisterResponseDto>("Error!");
            }
        }

        public async Task<Response<LoginResponseDto>> Login(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                    return new Response<LoginResponseDto>("invalid email or password!");

                var ok = await _userManager.CheckPasswordAsync(user, password);

                if (!ok)
                    return new Response<LoginResponseDto>("invalid email or passowrd!");


                return new Response<LoginResponseDto>(new LoginResponseDto { Token = await GenerateBearerTokenForAUserAsync(user) });
            }
            catch (Exception)
            {
                return new Response<LoginResponseDto>("Error!");
            }
        }
        private async Task<string> GenerateBearerTokenForAUserAsync(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim("UserId",value:user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone,value:user.PhoneNumber),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var jwt = new JwtSecurityToken(
                null,
                null,
                claims: authClaims,
                DateTime.Now,
                expires: DateTime.Now.AddTicks(_jwtSettings.TokenLifeTime.Ticks),
                signingCredentials: new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

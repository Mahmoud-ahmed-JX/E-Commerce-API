using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UnauthorizedException();
          var res=  await _userManager.CheckPasswordAsync(user, loginDto.Password);
        
            if(!res) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName, user.Email, "Token");
        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            User user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber

            };

            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors=result.Errors.Select(e=>e.Description).ToList();
            }
            return new UserResultDto(user.DisplayName, user.Email, "Token");
        }

        private async Task<string> CreateToken(User user)
        {
            // Token creation logic goes here
            var Claims = new List<Claim>() { 
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fa284bb363c969fd36007941a34e082b91382595bd5a252e8f62445a78a3ef25"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                issuer: "https://localhost:7080",
                audience: "angularproject",
                claims:Claims,
                expires:DateTime.Now.AddDays(30),
                signingCredentials:creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

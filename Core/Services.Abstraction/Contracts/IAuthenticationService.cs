using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
         Task<UserResultDto> Login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);
    }
}

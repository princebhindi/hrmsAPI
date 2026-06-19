using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, APIResponse<AuthResponseDto>>
    {

        private readonly IUserRepo repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public LoginUserHandler(IUserRepo _repo,IMapper mapper, IConfiguration configuration)
        {
            repo = _repo;
            _mapper = mapper;
            _configuration = configuration;
        }
        public string generateToken(UserRegister user)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
            var key = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
           issuer: _configuration["Jwt:Issuer"],
           audience: _configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(
               Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
           signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<APIResponse<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Register.DOMAIN.Entities.UserLogin>(request.user);
            var success = await repo.LoginUser(data);
            
            if (success!=null)
            {
               
               string token= generateToken(success);

                return new APIResponse<AuthResponseDto>
                {
                    Sucess = true,
                    Message = "Login successful",
                    Data = new AuthResponseDto
                    {
                        UserName = request.user.UserName,
                        Token = token
                    }
                };
            }

            return new APIResponse<AuthResponseDto>
            {
                Sucess = false,
                Message = "Invalid username or password",
                Data = null
            };
        }
    }
}

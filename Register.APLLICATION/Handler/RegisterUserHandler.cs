using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, APIResponse<UserRegisterDto>>
    {
        private readonly IUserRepo _Repo;
        private readonly IMapper _Mapper;
        public RegisterUserHandler(IUserRepo repo,IMapper Mapper)
        {
            _Repo = repo;
            _Mapper = Mapper;
        }

        public async Task<APIResponse<UserRegisterDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<object>();
            var hashpassword = request.User.Password;
            request.User.Password = passwordHasher.HashPassword(null, hashpassword);

            await _Repo.RegisterUser(request.User);

            return new APIResponse<UserRegisterDto>
            {
                Sucess = true,
                Message = "Data Inserted",
                Data = request.User
            };
        }
    }
}

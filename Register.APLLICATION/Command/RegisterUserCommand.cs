using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Register.APPLICATION.DTO;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Command
{
    public record RegisterUserCommand(UserRegisterDto User) : IRequest<APIResponse<UserRegisterDto>>;
}

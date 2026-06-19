using System;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Command
{
    public record UpdateUserCommand(UserRegisterDto User) : IRequest<APIResponse<bool>>;

    public record DeleteUserCommand(Guid Id) : IRequest<APIResponse<bool>>;
}

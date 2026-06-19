using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Queries
{
    public record GetAllUsersQuery() : IRequest<APIResponse<IEnumerable<UserRegisterDto>>>;

    public record GetUserByIdQuery(Guid Id) : IRequest<APIResponse<UserRegisterDto>>;
}

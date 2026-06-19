using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Command
{
    public record LoginUserCommand(Register.APPLICATION.DTO.UserLogin user) : IRequest<APIResponse<AuthResponseDto>>;
    
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, APIResponse<IEnumerable<UserRegisterDto>>>
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<UserRegisterDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetAllUsersAsync();
            var dtoList = _mapper.Map<IEnumerable<UserRegisterDto>>(users);

            return new APIResponse<IEnumerable<UserRegisterDto>>
            {
                Sucess = true,
                Message = "Users retrieved successfully",
                Data = dtoList
            };
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, APIResponse<UserRegisterDto>>
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<UserRegisterDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return new APIResponse<UserRegisterDto>
                {
                    Sucess = false,
                    Message = "User not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<UserRegisterDto>(user);
            return new APIResponse<UserRegisterDto>
            {
                Sucess = true,
                Message = "User retrieved successfully",
                Data = dto
            };
        }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, APIResponse<bool>>
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<UserRegister>(request.User);
            var result = await _repo.UpdateUserAsync(userEntity);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "User updated successfully" : "User not found or update failed",
                Data = result
            };
        }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, APIResponse<bool>>
    {
        private readonly IUserRepo _repo;

        public DeleteUserHandler(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteUserAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "User deleted successfully" : "User not found or deletion failed",
                Data = result
            };
        }
    }
}

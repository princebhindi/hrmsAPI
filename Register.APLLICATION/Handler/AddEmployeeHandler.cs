using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, APIResponse<EmployeeDto>>
    {
        private readonly IEmployee _repo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public AddEmployeeHandler(IEmployee repo, IUserRepo userRepo, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<APIResponse<EmployeeDto>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = request.Employee.Id == Guid.Empty ? Guid.NewGuid() : request.Employee.Id;
            request.Employee.Id = employeeId;

            var employee = _mapper.Map<Employee>(request.Employee);
            var result = await _repo.AddEmployeeAsync(employee);

            if (!string.IsNullOrEmpty(request.Employee.UserName) && !string.IsNullOrEmpty(request.Employee.Password))
            {
                var passwordHasher = new PasswordHasher<object>();
                var hashedPassword = passwordHasher.HashPassword(null, request.Employee.Password);

                var userRegisterDto = new UserRegisterDto
                {
                    Id = employeeId,
                    UserName = request.Employee.UserName,
                    Password = hashedPassword,
                    Role = request.Employee.Role ?? "Employee"
                };

                await _userRepo.RegisterUser(userRegisterDto);
            }

            var resultDto = _mapper.Map<EmployeeDto>(result);

            return new APIResponse<EmployeeDto>
            {
                Sucess = true,
                Message = "Employee added successfully",
                Data = resultDto
            };
        }
    }
}

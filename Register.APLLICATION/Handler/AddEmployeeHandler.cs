using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, APIResponse<EmployeeDto>>
    {
        private readonly IEmployee _repo;
        private readonly IMapper _mapper;

        public AddEmployeeHandler(IEmployee repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<EmployeeDto>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.Employee);
            var result = await _repo.AddEmployeeAsync(employee);
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

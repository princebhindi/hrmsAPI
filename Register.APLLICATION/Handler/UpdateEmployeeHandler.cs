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
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, APIResponse<bool>>
    {
        private readonly IEmployee _repo;
        private readonly IMapper _mapper;

        public UpdateEmployeeHandler(IEmployee repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.Employee);
            var success = await _repo.UpdateEmployeeAsync(employee);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Employee updated successfully" : "Employee not found",
                Data = success
            };
        }
    }
}

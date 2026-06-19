using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, APIResponse<EmployeeDto>>
    {
        private readonly IEmployee _repo;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IEmployee repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repo.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
            {
                return new APIResponse<EmployeeDto>
                {
                    Sucess = false,
                    Message = "Employee not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<EmployeeDto>(employee);

            return new APIResponse<EmployeeDto>
            {
                Sucess = true,
                Message = "Employee retrieved successfully",
                Data = dto
            };
        }
    }
}

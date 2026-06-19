using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, APIResponse<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployee _repo;
        private readonly IMapper _mapper;

        public GetAllEmployeesHandler(IEmployee repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repo.GetAllEmployeesAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new APIResponse<IEnumerable<EmployeeDto>>
            {
                Sucess = true,
                Message = "Employees retrieved successfully",
                Data = dtos
            };
        }
    }
}

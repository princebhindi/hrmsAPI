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
    public class GetAllSalariesHandler : IRequestHandler<GetAllSalariesQuery, APIResponse<IEnumerable<SalaryResponseDto>>>
    {
        private readonly ISalary _repo;
        private readonly IMapper _mapper;

        public GetAllSalariesHandler(ISalary repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<SalaryResponseDto>>> Handle(GetAllSalariesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllSalariesAsync(request.PageNumber, request.PageSize);
            var resultDto = _mapper.Map<IEnumerable<SalaryResponseDto>>(result);

            return new APIResponse<IEnumerable<SalaryResponseDto>>
            {
                Sucess = true,
                Message = "Salaries retrieved successfully",
                Data = resultDto
            };
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetSalaryByIdHandler : IRequestHandler<GetSalaryByIdQuery, APIResponse<SalaryResponseDto>>
    {
        private readonly ISalary _repo;
        private readonly IMapper _mapper;

        public GetSalaryByIdHandler(ISalary repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<SalaryResponseDto>> Handle(GetSalaryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetSalaryByIdAsync(request.Id);
            if (result == null)
            {
                return new APIResponse<SalaryResponseDto>
                {
                    Sucess = false,
                    Message = "Salary record not found",
                    Data = null
                };
            }

            var resultDto = _mapper.Map<SalaryResponseDto>(result);
            return new APIResponse<SalaryResponseDto>
            {
                Sucess = true,
                Message = "Salary record retrieved successfully",
                Data = resultDto
            };
        }
    }
}

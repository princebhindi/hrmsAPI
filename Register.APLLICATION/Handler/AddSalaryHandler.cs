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
    public class AddSalaryHandler : IRequestHandler<AddSalaryCommand, APIResponse<SalaryDto>>
    {
        private readonly ISalary _repo;
        private readonly IMapper _mapper;

        public AddSalaryHandler(ISalary repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<SalaryDto>> Handle(AddSalaryCommand request, CancellationToken cancellationToken)
        {
            var salary = _mapper.Map<Salary>(request.Salary);
            var result = await _repo.AddSalaryAsync(salary);
            var resultDto = _mapper.Map<SalaryDto>(result);

            return new APIResponse<SalaryDto>
            {
                Sucess = true,
                Message = "Salary added and processed successfully",
                Data = resultDto
            };
        }
    }
}

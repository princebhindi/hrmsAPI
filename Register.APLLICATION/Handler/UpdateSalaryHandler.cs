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
    public class UpdateSalaryHandler : IRequestHandler<UpdateSalaryCommand, APIResponse<bool>>
    {
        private readonly ISalary _repo;
        private readonly IMapper _mapper;

        public UpdateSalaryHandler(ISalary repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateSalaryCommand request, CancellationToken cancellationToken)
        {
            var salary = _mapper.Map<Salary>(request.Salary);
            var result = await _repo.UpdateSalaryAsync(salary);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Salary updated successfully" : "Salary not found",
                Data = result
            };
        }
    }
}

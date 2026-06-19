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
    public class AddJobHandler : IRequestHandler<AddJobCommand, APIResponse<JobDto>>
    {
        private readonly IJob _repo;
        private readonly IMapper _mapper;

        public AddJobHandler(IJob repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<JobDto>> Handle(AddJobCommand request, CancellationToken cancellationToken)
        {
            var job = _mapper.Map<Job>(request.Job);
            var result = await _repo.AddJobAsync(job);
            var resultDto = _mapper.Map<JobDto>(result);

            return new APIResponse<JobDto>
            {
                Sucess = true,
                Message = "Job vacancy added successfully",
                Data = resultDto
            };
        }
    }
}

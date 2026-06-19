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
    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, APIResponse<bool>>
    {
        private readonly IJob _repo;
        private readonly IMapper _mapper;

        public UpdateJobHandler(IJob repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _mapper.Map<Job>(request.Job);
            var success = await _repo.UpdateJobAsync(job);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Job updated successfully" : "Job not found",
                Data = success
            };
        }
    }
}

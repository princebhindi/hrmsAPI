using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, APIResponse<JobDto>>
    {
        private readonly IJob _repo;
        private readonly IMapper _mapper;

        public GetJobByIdHandler(IJob repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<JobDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _repo.GetJobByIdAsync(request.Id);
            if (job == null)
            {
                return new APIResponse<JobDto>
                {
                    Sucess = false,
                    Message = "Job not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<JobDto>(job);

            return new APIResponse<JobDto>
            {
                Sucess = true,
                Message = "Job retrieved successfully",
                Data = dto
            };
        }
    }
}

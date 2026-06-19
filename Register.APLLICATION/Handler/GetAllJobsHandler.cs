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
    public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, APIResponse<IEnumerable<JobDto>>>
    {
        private readonly IJob _repo;
        private readonly IMapper _mapper;

        public GetAllJobsHandler(IJob repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<JobDto>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _repo.GetAllJobsAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<JobDto>>(jobs);

            return new APIResponse<IEnumerable<JobDto>>
            {
                Sucess = true,
                Message = "Jobs retrieved successfully",
                Data = dtos
            };
        }
    }
}

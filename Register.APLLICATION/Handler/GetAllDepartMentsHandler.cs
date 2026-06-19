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
    public class GetAllDepartMentsHandler : IRequestHandler<GetAllDepartMentsQuery, APIResponse<IEnumerable<DepartMentDto>>>
    {
        private readonly IDepartMent _repo;
        private readonly IMapper _mapper;

        public GetAllDepartMentsHandler(IDepartMent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<DepartMentDto>>> Handle(GetAllDepartMentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repo.GetAllDepartmentsAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<DepartMentDto>>(departments);

            return new APIResponse<IEnumerable<DepartMentDto>>
            {
                Sucess = true,
                Message = "Departments retrieved successfully",
                Data = dtos
            };
        }
    }
}

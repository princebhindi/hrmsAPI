using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetDepartMentByIdHandler : IRequestHandler<GetDepartMentByIdQuery, APIResponse<DepartMentDto>>
    {
        private readonly IDepartMent _repo;
        private readonly IMapper _mapper;

        public GetDepartMentByIdHandler(IDepartMent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<DepartMentDto>> Handle(GetDepartMentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _repo.GetDepartmentByIdAsync(request.Id);
            if (department == null)
            {
                return new APIResponse<DepartMentDto>
                {
                    Sucess = false,
                    Message = "Department not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<DepartMentDto>(department);

            return new APIResponse<DepartMentDto>
            {
                Sucess = true,
                Message = "Department retrieved successfully",
                Data = dto
            };
        }
    }
}

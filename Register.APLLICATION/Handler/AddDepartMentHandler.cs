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
    public class AddDepartMentHandler : IRequestHandler<AddDepartMentCommand, APIResponse<DepartMentDto>>
    {
        private readonly IDepartMent _repo;
        private readonly IMapper _mapper;

        public AddDepartMentHandler(IDepartMent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<DepartMentDto>> Handle(AddDepartMentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<DepartMent>(request.DepartMent);
            var result = await _repo.AddDepartmentAsync(department);
            var resultDto = _mapper.Map<DepartMentDto>(result);

            return new APIResponse<DepartMentDto>
            {
                Sucess = true,
                Message = "Department added successfully",
                Data = resultDto
            };
        }
    }
}

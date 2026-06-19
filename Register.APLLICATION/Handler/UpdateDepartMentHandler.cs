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
    public class UpdateDepartMentHandler : IRequestHandler<UpdateDepartMentCommand, APIResponse<bool>>
    {
        private readonly IDepartMent _repo;
        private readonly IMapper _mapper;

        public UpdateDepartMentHandler(IDepartMent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateDepartMentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<DepartMent>(request.DepartMent);
            var success = await _repo.UpdateDepartmentAsync(department);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Department updated successfully" : "Department not found",
                Data = success
            };
        }
    }
}

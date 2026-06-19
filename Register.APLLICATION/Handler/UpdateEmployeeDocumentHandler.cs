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
    public class UpdateEmployeeDocumentHandler : IRequestHandler<UpdateEmployeeDocumentCommand, APIResponse<bool>>
    {
        private readonly IEmployeeDocument _repo;
        private readonly IMapper _mapper;

        public UpdateEmployeeDocumentHandler(IEmployeeDocument repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateEmployeeDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<EmployeeDocument>(request.EmployeeDocument);
            var result = await _repo.UpdateEmployeeDocumentAsync(document);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Employee document updated successfully" : "Employee document not found",
                Data = result
            };
        }
    }
}

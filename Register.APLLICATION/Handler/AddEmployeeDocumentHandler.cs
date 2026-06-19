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
    public class AddEmployeeDocumentHandler : IRequestHandler<AddEmployeeDocumentCommand, APIResponse<EmployeeDocumentDto>>
    {
        private readonly IEmployeeDocument _repo;
        private readonly IMapper _mapper;

        public AddEmployeeDocumentHandler(IEmployeeDocument repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<EmployeeDocumentDto>> Handle(AddEmployeeDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<EmployeeDocument>(request.EmployeeDocument);
            var result = await _repo.AddEmployeeDocumentAsync(document);
            var resultDto = _mapper.Map<EmployeeDocumentDto>(result);

            return new APIResponse<EmployeeDocumentDto>
            {
                Sucess = true,
                Message = "Employee document added successfully",
                Data = resultDto
            };
        }
    }
}

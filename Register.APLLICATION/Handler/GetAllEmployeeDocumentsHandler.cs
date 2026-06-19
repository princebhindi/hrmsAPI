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
    public class GetAllEmployeeDocumentsHandler : IRequestHandler<GetAllEmployeeDocumentsQuery, APIResponse<IEnumerable<EmployeeDocumentResponseDto>>>
    {
        private readonly IEmployeeDocument _repo;
        private readonly IMapper _mapper;

        public GetAllEmployeeDocumentsHandler(IEmployeeDocument repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<EmployeeDocumentResponseDto>>> Handle(GetAllEmployeeDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await _repo.GetAllEmployeeDocumentsAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<EmployeeDocumentResponseDto>>(documents);

            return new APIResponse<IEnumerable<EmployeeDocumentResponseDto>>
            {
                Sucess = true,
                Message = "Employee documents retrieved successfully",
                Data = dtos
            };
        }
    }
}

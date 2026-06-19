using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetEmployeeDocumentByIdHandler : IRequestHandler<GetEmployeeDocumentByIdQuery, APIResponse<EmployeeDocumentResponseDto>>
    {
        private readonly IEmployeeDocument _repo;
        private readonly IMapper _mapper;

        public GetEmployeeDocumentByIdHandler(IEmployeeDocument repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<EmployeeDocumentResponseDto>> Handle(GetEmployeeDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _repo.GetEmployeeDocumentByIdAsync(request.Id);
            if (document == null)
            {
                return new APIResponse<EmployeeDocumentResponseDto>
                {
                    Sucess = false,
                    Message = "Employee document not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<EmployeeDocumentResponseDto>(document);
            return new APIResponse<EmployeeDocumentResponseDto>
            {
                Sucess = true,
                Message = "Employee document retrieved successfully",
                Data = dto
            };
        }
    }
}

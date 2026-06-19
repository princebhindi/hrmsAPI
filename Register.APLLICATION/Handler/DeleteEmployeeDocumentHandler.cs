using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteEmployeeDocumentHandler : IRequestHandler<DeleteEmployeeDocumentCommand, APIResponse<bool>>
    {
        private readonly IEmployeeDocument _repo;

        public DeleteEmployeeDocumentHandler(IEmployeeDocument repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteEmployeeDocumentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteEmployeeDocumentAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Employee document deleted successfully" : "Employee document not found",
                Data = result
            };
        }
    }
}

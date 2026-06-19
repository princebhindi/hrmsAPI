using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetNoticeCountHandler : IRequestHandler<GetNoticeCountQuery, APIResponse<int>>
    {
        private readonly INotice _repo;

        public GetNoticeCountHandler(INotice repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetNoticeCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetNoticeCountAsync();
            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Notice count retrieved successfully",
                Data = count
            };
        }
    }
}

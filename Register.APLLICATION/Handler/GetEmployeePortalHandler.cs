using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetEmployeePortalHandler : IRequestHandler<GetEmployeePortalDataQueries, APIResponse<EmployeeStatsDto>>
    {
        private readonly IEmployeeStatRepo _repo;
        private readonly IMapper _mapper;

        public GetEmployeePortalHandler(IEmployeeStatRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<APIResponse<EmployeeStatsDto>> Handle(GetEmployeePortalDataQueries request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetEmployeeStateData(request.EmpId);

            return new APIResponse<EmployeeStatsDto>
            {
                Sucess = true,
                Message = "Data Fetched EmployeeState",
                Data = res
            };
        }
    }
}


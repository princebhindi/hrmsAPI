using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Register.APPLICATION.DTO;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Mapping
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<UserRegister, UserRegisterDto>().ReverseMap();
            CreateMap<Register.DOMAIN.Entities.UserLogin, Register.APPLICATION.DTO.UserLogin>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<DepartMent, DepartMentDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Leave, LeaveResponseDto>().ReverseMap();
            CreateMap<Leave, LeavesDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Attendance, AttendanceResponseDto>().ReverseMap();
            CreateMap<EmployeeDocument, EmployeeDocumentDto>().ReverseMap();
            CreateMap<EmployeeDocument, EmployeeDocumentResponseDto>().ReverseMap();
            CreateMap<Notice, NoticeDto>().ReverseMap();
            CreateMap<Notice, NoticeResponseDto>().ReverseMap();
            CreateMap<Salary, SalaryDto>().ReverseMap();
            CreateMap<Salary, SalaryResponseDto>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeStatsDto>().ReverseMap();
        }
    }
}

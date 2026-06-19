using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Register.DOMAIN.Entities;

namespace Register.PERSISTANCE.Context
{
    public class ApllicationDbContext:DbContext
    {
        public ApllicationDbContext(DbContextOptions<ApllicationDbContext> options):base(options)
        {

        }

        public DbSet<UserRegister> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DepartMent> DepartMents { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
}

using FluentValidation;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Validators
{
    public class AttendanceDtoValidator : AbstractValidator<AttendanceDto>
    {
        public AttendanceDtoValidator()
        {
            RuleFor(x => x.EmpId)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(20).WithMessage("Status cannot exceed 20 characters.");
        }
    }
}

using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
    {
        public UpdateAttendanceCommandValidator()
        {
            RuleFor(x => x.Attendance)
                .NotNull().WithMessage("Attendance data cannot be null.")
                .SetValidator(new AttendanceDtoValidator());
        }
    }
}

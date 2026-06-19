using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddAttendanceCommandValidator : AbstractValidator<AddAttendanceCommand>
    {
        public AddAttendanceCommandValidator()
        {
            RuleFor(x => x.Attendance)
                .NotNull().WithMessage("Attendance data cannot be null.")
                .SetValidator(new AttendanceDtoValidator());
        }
    }
}

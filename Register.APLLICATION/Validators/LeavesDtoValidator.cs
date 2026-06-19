using FluentValidation;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Validators
{
    public class LeavesDtoValidator : AbstractValidator<LeavesDto>
    {
        public LeavesDtoValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End Date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End Date must be greater than or equal to Start Date.");

            RuleFor(x => x.Reason)
                .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters.");
        }
    }
}

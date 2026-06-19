using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddJobCommandValidator : AbstractValidator<AddJobCommand>
    {
        public AddJobCommandValidator()
        {
            RuleFor(x => x.Job)
                .NotNull().WithMessage("Job data cannot be null.")
                .SetValidator(new JobDtoValidator());
        }
    }
}

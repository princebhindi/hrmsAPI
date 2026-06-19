using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Job)
                .NotNull().WithMessage("Job data cannot be null.")
                .SetValidator(new JobDtoValidator());
        }
    }
}

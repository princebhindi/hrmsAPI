using FluentValidation;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Validators
{
    public class JobDtoValidator : AbstractValidator<JobDto>
    {
        public JobDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Job Title is required.")
                .MaximumLength(100).WithMessage("Job Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Job Description is required.");

            RuleFor(x => x.DeptId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }
}

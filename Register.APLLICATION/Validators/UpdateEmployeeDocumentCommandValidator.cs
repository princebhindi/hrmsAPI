using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateEmployeeDocumentCommandValidator : AbstractValidator<UpdateEmployeeDocumentCommand>
    {
        public UpdateEmployeeDocumentCommandValidator()
        {
            RuleFor(x => x.EmployeeDocument)
                .NotNull().WithMessage("Employee Document data cannot be null.")
                .SetValidator(new EmployeeDocumentDtoValidator());
        }
    }
}

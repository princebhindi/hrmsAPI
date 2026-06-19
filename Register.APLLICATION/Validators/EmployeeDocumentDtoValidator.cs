using FluentValidation;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Validators
{
    public class EmployeeDocumentDtoValidator : AbstractValidator<EmployeeDocumentDto>
    {
        public EmployeeDocumentDtoValidator()
        {
            RuleFor(x => x.EmpId)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.DocumentType)
                .NotEmpty().WithMessage("Document Type is required.")
                .MaximumLength(50).WithMessage("Document Type cannot exceed 50 characters.");

            RuleFor(x => x.DocumentName)
                .NotEmpty().WithMessage("Document Name is required.")
                .MaximumLength(150).WithMessage("Document Name cannot exceed 150 characters.");

            RuleFor(x => x.DocumentPath)
                .NotEmpty().WithMessage("Document Path is required.");
        }
    }
}

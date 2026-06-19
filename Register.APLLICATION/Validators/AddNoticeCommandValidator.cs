using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class AddNoticeCommandValidator : AbstractValidator<AddNoticeCommand>
    {
        public AddNoticeCommandValidator()
        {
            RuleFor(x => x.Notice)
                .NotNull().WithMessage("Notice data cannot be null.")
                .SetValidator(new NoticeDtoValidator());
        }
    }
}

using FluentValidation;
using Register.APPLICATION.Command;

namespace Register.APPLICATION.Validators
{
    public class UpdateNoticeCommandValidator : AbstractValidator<UpdateNoticeCommand>
    {
        public UpdateNoticeCommandValidator()
        {
            RuleFor(x => x.Notice)
                .NotNull().WithMessage("Notice data cannot be null.")
                .SetValidator(new NoticeDtoValidator());
        }
    }
}

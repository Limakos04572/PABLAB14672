using FluentValidation;

namespace ProductsApp.Application.Commands.Identity.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Confirm password is required.");
    }
}

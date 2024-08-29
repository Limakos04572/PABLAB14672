using FluentValidation;

namespace PabLab.Application.Commands.Student.AddStudent;

public class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
{
    public AddStudentCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required.")
            .MinimumLength(3).WithMessage("Firstname cannot be shorter the 3 character.")
            .MaximumLength(200).WithMessage("Firstname cannot be longer the 200 character.");
            
        RuleFor(x => x.Surename)
            .NotEmpty().WithMessage("Surename is required.")
            .MinimumLength(3).WithMessage("Surename cannot be shorter the 3 character.")
            .MaximumLength(200).WithMessage("Surename cannot be longer the 200 character.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.");
    }
}

using FluentValidation;

namespace PabLab.Application.Commands.Enrollment.AddEnrollment;

public class AddEnrollmentCommandValidator : AbstractValidator<AddEnrollmentCommand>
{
    public AddEnrollmentCommandValidator()
    {
        RuleFor(x => x.IdStudent)
            .NotEmpty().WithMessage("Student id is required.");
            
        RuleFor(x => x.IdCourse)
            .NotEmpty().WithMessage("Course id is required.");
    }
}

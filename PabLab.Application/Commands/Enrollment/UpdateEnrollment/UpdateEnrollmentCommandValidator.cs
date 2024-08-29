using FluentValidation;
using PabLab.Application.Commands.Student.UpdateStudent;

namespace PabLab.Application.Commands.Enrollment.UpdateEnrollment;

public class UpdateEnrollmentCommandValidator : AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentCommandValidator()
    {
        RuleFor(x => x.IdEnrollment)
            .NotEmpty().WithMessage("Enrollment id is required.");

        RuleFor(x => x.IdStudent)
            .NotEmpty().WithMessage("Student id is required.");

        RuleFor(x => x.IdCourse)
            .NotEmpty().WithMessage("Course id is required.");
    }
}

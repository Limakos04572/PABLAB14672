using FluentValidation;

namespace PabLab.Application.Commands.Course.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title cannot be shorter the 3 character.")
                .MaximumLength(200).WithMessage("Title cannot be longer the 200 character.");

            RuleFor(x => x.Owner)
                .GreaterThan(0).WithMessage("Credits cannot be less than 0.");
        }
    }
}

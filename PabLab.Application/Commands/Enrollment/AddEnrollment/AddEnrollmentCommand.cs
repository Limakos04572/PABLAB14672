using MediatR;

namespace PabLab.Application.Commands.Enrollment.AddEnrollment;

public class AddEnrollmentCommand : IRequest
{
    public int IdStudent { get; set; }
    
    public int IdCourse { get; set; }
}

using MediatR;

namespace PabLab.Application.Commands.Enrollment.UpdateEnrollment
{
    public class UpdateEnrollmentCommand : IRequest
    {
        public int IdEnrollment { get; set; }
        public int IdStudent { get; set; }
    
        public int IdCourse { get; set; }
    }
}

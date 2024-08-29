using MediatR;

namespace PabLab.Application.Commands.Course.UpdateCourse
{
    public class UpdateCourseCommand : IRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Owner { get; set; }
    }
}

using MediatR;

namespace PabLab.Application.Commands.Course.AddCourse;

public class AddCourseCommand : IRequest
{
    public string Name { get; set; }
    public int Owner { get; set; }
}

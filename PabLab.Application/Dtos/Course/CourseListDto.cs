namespace PabLab.Application.Dtos.Course;

public class CourseListDto
{
    public int Count { get; set; }
    
    public IEnumerable<CourseDto> Courses { get; set; }
}
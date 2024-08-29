using PabLab.Application.Dtos.Course;

namespace PabLab.Application.Dtos.Enrollment;

public class EnrollmentListDto
{
    public int Count { get; set; }
    
    public IEnumerable<EnrollmentDto> Enrollments { get; set; }
}
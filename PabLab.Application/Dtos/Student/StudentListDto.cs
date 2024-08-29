namespace PabLab.Application.Dtos.Student;

public class StudentListDto
{
    public int Count { get; set; }
    
    public IEnumerable<StudentDto> Students { get; set; }
}
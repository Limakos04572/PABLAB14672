using MediatR;

namespace PabLab.Application.Commands.Student.AddStudent;

public class AddStudentCommand : IRequest
{
    public string FirstName { get; set; }
    
    public string Surename { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

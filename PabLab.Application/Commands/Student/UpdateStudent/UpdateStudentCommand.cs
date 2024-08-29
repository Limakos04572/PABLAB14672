using MediatR;

namespace PabLab.Application.Commands.Student.UpdateStudent
{
    public class UpdateStudentCommand : IRequest
    {
        public int IdStudent { get; set; }
        
        public string FirstName { get; set; }
    
        public string Surename { get; set; }
    
        public DateTime DateOfBirth { get; set; }
    }
}

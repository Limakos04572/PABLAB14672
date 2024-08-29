using MediatR;

namespace PabLab.Application.Commands.Student.RemoveStudent;

public record RemoveStudentCommand(int Id) : IRequest;

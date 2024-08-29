using MediatR;
using PabLab.Application.Dtos.Student;

namespace PabLab.Application.Queries.Student.GetStudents;

public record GetStudentsQuery() : IRequest<StudentListDto>; 

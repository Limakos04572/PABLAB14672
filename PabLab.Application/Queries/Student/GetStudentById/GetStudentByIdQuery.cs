using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Application.Dtos.Student;

namespace PabLab.Application.Queries.Student.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto>;

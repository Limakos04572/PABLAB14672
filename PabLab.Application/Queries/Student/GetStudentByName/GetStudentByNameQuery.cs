using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Application.Dtos.Student;

namespace PabLab.Application.Queries.Student.GetStudentByName;

public record GetStudentByNameQuery(string FirstName, string LastName) : IRequest<StudentDto>;


using MediatR;
using PabLab.Application.Dtos.Course;

namespace PabLab.Application.Queries.Course.GetCourseById;

public record GetCourseByIdQuery(int Id) : IRequest<CourseDto>;

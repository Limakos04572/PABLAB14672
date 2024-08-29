using MediatR;
using PabLab.Application.Dtos.Course;

namespace PabLab.Application.Queries.Course.GetCourses;

public record GetCoursesQuery() : IRequest<CourseListDto>; 

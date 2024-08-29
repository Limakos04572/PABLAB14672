using MediatR;

namespace PabLab.Application.Commands.Course.RemoveCourse;

public record RemoveCourseCommand(int Id) : IRequest;

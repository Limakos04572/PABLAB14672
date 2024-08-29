using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Course.GetCourseById;

internal class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        var courseDto = _mapper.Map<CourseDto>(course);

        return courseDto;
    }
}

using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Course.GetCourseByName;

internal class GetCourseByNameQueryHandler : IRequestHandler<GetCourseByNameQuery, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseByNameQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseByNameQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByTitleAsync(request.Name, cancellationToken);

        var courseDto = _mapper.Map<CourseDto>(course);

        return courseDto;
    }
}

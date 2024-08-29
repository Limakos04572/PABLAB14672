using AutoMapper;
using PabLab.Application.Dtos.Course;
using PabLab.Domain.Entities;

namespace PabLab.Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<IEnumerable<Course>, CourseListDto>()
            .ForMember(dest => dest.Count, conf => conf.MapFrom(model => model.Count()))
            .ForMember(dest => dest.Courses, conf => conf.MapFrom(model => model));

    }
}
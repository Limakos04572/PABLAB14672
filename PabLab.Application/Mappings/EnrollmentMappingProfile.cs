using AutoMapper;
using PabLab.Application.Dtos.Enrollment;
using PabLab.Domain.Entities;

namespace PabLab.Application.Mappings;

public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<Enrollment, EnrollmentDto>();
        CreateMap<IEnumerable<Enrollment>, EnrollmentListDto>()
            .ForMember(dest => dest.Count, conf => conf.MapFrom(model => model.Count()))
            .ForMember(dest => dest.Enrollments, conf => conf.MapFrom(model => model));
    }
}
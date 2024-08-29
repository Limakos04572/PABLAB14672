using AutoMapper;
using PabLab.Application.Dtos.Student;
using PabLab.Domain.Entities;

namespace PabLab.Application.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<IEnumerable<Student>, StudentListDto>()
            .ForMember(dest => dest.Count, conf => conf.MapFrom(model => model.Count()))
            .ForMember(dest => dest.Students, conf => conf.MapFrom(model => model));
    }
}
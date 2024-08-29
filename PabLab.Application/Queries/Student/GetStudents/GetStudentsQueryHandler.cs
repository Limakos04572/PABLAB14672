using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Application.Dtos.Student;
using PabLab.Application.Queries.Course.GetCourses;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Student.GetStudents;

internal class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, StudentListDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<StudentListDto> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllAsync(cancellationToken);

        var studentsDto = _mapper.Map<StudentListDto>(students);

        return studentsDto;
    }
}

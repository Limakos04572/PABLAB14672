using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Course;
using PabLab.Application.Dtos.Student;
using PabLab.Application.Queries.Course.GetCourseByName;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Student.GetStudentByName;

internal class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, StudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentByNameQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByNameAsync(request.FirstName, request.LastName, cancellationToken);

        var studentDto = _mapper.Map<StudentDto>(student);

        return studentDto;
    }
}

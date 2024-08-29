using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Enrollment.AddEnrollment;

internal class AddEnrollmentCommandHandler : IRequestHandler<AddEnrollmentCommand>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddEnrollmentCommandHandler
    (
        IEnrollmentRepository enrollmentRepository, 
        IStudentRepository studentRepository, 
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
    {
        bool isCourseExist = await _courseRepository.ExistsAsync(request.IdCourse, cancellationToken);
        if (!isCourseExist)
            throw new NotFoundException(request.IdCourse);
        
        bool isStudentExist = await _courseRepository.ExistsAsync(request.IdStudent, cancellationToken);
        if (!isStudentExist)
            throw new NotFoundException(request.IdStudent);
        
        var newEnrollment = new Domain.Entities.Enrollment()
        {
            IdStudent = request.IdStudent,
            IdCourse = request.IdCourse,
            DateEnrollment = DateTime.Now
        };

        _enrollmentRepository.Add(newEnrollment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

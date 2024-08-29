using MediatR;
using PabLab.Application.Commands.Student.UpdateStudent;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Enrollment.UpdateEnrollment
{
    internal class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEnrollmentCommandHandler
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

        public async Task Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrl = await _enrollmentRepository.GetByIdAsync(request.IdEnrollment, cancellationToken);
            if (enrl == null)
                throw new NotFoundException(request.IdEnrollment);

            if (enrl.IdStudent != request.IdStudent)
            {
                bool isStudentExist = await _courseRepository.ExistsAsync(request.IdStudent, cancellationToken);
                if (!isStudentExist)
                    throw new NotFoundException(request.IdStudent);
            }
            
            if (enrl.IdCourse != request.IdCourse)
            {
                bool isCourseExist = await _courseRepository.ExistsAsync(request.IdCourse, cancellationToken);
                if (!isCourseExist)
                    throw new NotFoundException(request.IdCourse);
            }

            enrl.IdStudent = request.IdStudent;
            enrl.IdCourse = request.IdCourse;

            _enrollmentRepository.Update(enrl);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

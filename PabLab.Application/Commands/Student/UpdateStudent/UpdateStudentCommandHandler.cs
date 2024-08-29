using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Student.UpdateStudent
{
    internal class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.IdStudent, cancellationToken);
            if (student == null)
                throw new NotFoundException(request.IdStudent);

            if (student.FirstName != request.FirstName || student.Surename != request.Surename)
            {
                bool isAlreadyExist = await _studentRepository.IsAlreadyExistAsync(request.FirstName, request.Surename, cancellationToken);
                if (isAlreadyExist)
                    throw new AlreadyExistsException(request.FirstName + request.Surename);
            }

            student.FirstName = request.FirstName;
            student.Surename = request.Surename;
            student.DateOfBirth = request.DateOfBirth;

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

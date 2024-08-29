using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Enrollment.RemoveEnrollment;

internal class RemoveEnrollmentCommandHandler : IRequestHandler<RemoveEnrollmentCommand>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrl = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enrl == null)
            throw new NotFoundException(request.Id);

        _enrollmentRepository.Delete(enrl);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

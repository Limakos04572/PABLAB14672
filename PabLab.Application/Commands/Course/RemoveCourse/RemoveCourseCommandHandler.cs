using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Course.RemoveCourse;

internal class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
    {
        var product = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new NotFoundException(request.Id);

        _courseRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Course.AddCourse;

internal class AddCourseCommandHandler : IRequestHandler<AddCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        bool isAlreadyExist = await _courseRepository.IsAlreadyExistAsync(request.Name, cancellationToken);
        if (isAlreadyExist) 
            throw new AlreadyExistsException(request.Name);

        var newCourse = new Domain.Entities.Course()
        {
            Name = request.Name,
            Owner = request.Owner
        };

        _courseRepository.Add(newCourse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

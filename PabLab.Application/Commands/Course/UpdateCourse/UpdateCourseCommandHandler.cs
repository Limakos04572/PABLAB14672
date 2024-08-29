using MediatR;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Exceptions;

namespace PabLab.Application.Commands.Course.UpdateCourse
{
    internal class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var product = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new NotFoundException(request.Id);

            if (product.Name != request.Name)
            {
                bool isExist = await _courseRepository.IsAlreadyExistAsync(request.Name, cancellationToken);
                if (isExist)
                    throw new AlreadyExistsException(request.Name);
            }

            product.Name = request.Name;
            product.Owner = request.Owner;

            _courseRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

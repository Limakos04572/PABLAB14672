using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Enrollment;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Enrollment.GetEnrollmentById;

internal class GetEnrollmentByIdQueryHandler : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IMapper _mapper;

    public GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }

    public async Task<EnrollmentDto> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);

        var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollment);

        return enrollmentDto;
    }
}

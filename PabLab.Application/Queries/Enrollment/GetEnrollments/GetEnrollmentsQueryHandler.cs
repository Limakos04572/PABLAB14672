using AutoMapper;
using MediatR;
using PabLab.Application.Dtos.Enrollment;
using PabLab.Domain.Abstractions;

namespace PabLab.Application.Queries.Enrollment.GetEnrollments;

internal class GetEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentsQuery, EnrollmentListDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IMapper _mapper;

    public GetEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }

    public async Task<EnrollmentListDto> Handle(GetEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);

        var enrollmentsDto = _mapper.Map<EnrollmentListDto>(enrollments);

        return enrollmentsDto;
    }
}

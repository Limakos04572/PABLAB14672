using MediatR;
using PabLab.Application.Dtos.Enrollment;

namespace PabLab.Application.Queries.Enrollment.GetEnrollments;

public record GetEnrollmentsQuery() : IRequest<EnrollmentListDto>; 

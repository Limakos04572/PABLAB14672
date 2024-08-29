using MediatR;
using PabLab.Application.Dtos.Enrollment;

namespace PabLab.Application.Queries.Enrollment.GetEnrollmentById;

public record GetEnrollmentByIdQuery(int Id) : IRequest<EnrollmentDto>;

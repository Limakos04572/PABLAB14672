using MediatR;

namespace PabLab.Application.Commands.Enrollment.RemoveEnrollment;

public record RemoveEnrollmentCommand(int Id) : IRequest;

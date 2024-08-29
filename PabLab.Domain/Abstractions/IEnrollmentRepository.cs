using PabLab.Domain.Entities;

namespace PabLab.Domain.Abstractions;

public interface IEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Enrollment> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Add(Enrollment enrollment);
    void Update(Enrollment enrollment);
    void Delete(Enrollment enrollment);
}
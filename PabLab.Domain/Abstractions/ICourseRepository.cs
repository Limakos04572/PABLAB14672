using PabLab.Domain.Entities;

namespace PabLab.Domain.Abstractions;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Course> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Course> GetByTitleAsync(string title, CancellationToken cancellationToken = default);
    Task<bool> IsAlreadyExistAsync(string title, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    void Add(Course course);
    void Update(Course course);
    void Delete(Course course);
}


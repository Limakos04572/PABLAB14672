using PabLab.Domain.Entities;

namespace PabLab.Domain.Abstractions;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Student> GetByNameAsync(string firstName, string lastName, CancellationToken cancellationToken = default);
    Task<bool> IsAlreadyExistAsync(string firstName, string lastName, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    void Add(Student student);
    void Update(Student student);
    void Delete(Student student);
}
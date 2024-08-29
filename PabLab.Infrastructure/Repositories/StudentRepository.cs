using Microsoft.EntityFrameworkCore;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Entities;
using PabLab.Infrastructure.Context;

namespace PabLab.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository {
    
    private readonly PabLabContext _context;

    public StudentRepository(PabLabContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Students.ToListAsync(cancellationToken);
    }

    public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id, cancellationToken);
    }

    public async Task<Student> GetByNameAsync(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .SingleOrDefaultAsync(x => x.FirstName == firstName && x.Surename == lastName, cancellationToken);
    }

    public async Task<bool> IsAlreadyExistAsync(string firstName, string lastName, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .AnyAsync(s => s.FirstName == firstName && s.Surename == lastName, cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .AnyAsync(s => s.StudentId == id, cancellationToken);
    }

    public void Add(Student student)
    {
        _context.Students.Add(student);
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public void Delete(Student student)
    {
        _context.Students.Remove(student);
    }
}
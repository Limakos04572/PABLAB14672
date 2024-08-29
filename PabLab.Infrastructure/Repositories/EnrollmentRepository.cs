using PabLab.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using PabLab.Domain.Entities;
using PabLab.Infrastructure.Context;

namespace PabLab.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly PabLabContext _context;

    public EnrollmentRepository(PabLabContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync(cancellationToken);
    }

    public async Task<Enrollment> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id, cancellationToken);
    }

    public void Add(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
    }

    public void Update(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
    }

    public void Delete(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
    }
}

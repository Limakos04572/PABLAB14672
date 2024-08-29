using Microsoft.EntityFrameworkCore;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Entities;
using PabLab.Infrastructure.Context;

namespace PabLab.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly PabLabContext _context;

    public CourseRepository(PabLabContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Courses.ToListAsync(cancellationToken);
    }

    public async Task<Course> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == id, cancellationToken);
    }
    
    public async Task<Course> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.SingleOrDefaultAsync(x => x.Name == title, cancellationToken);
    }

    public async Task<bool> IsAlreadyExistAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AnyAsync(c => c.Name == title, cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AnyAsync(c => c.CourseId == id, cancellationToken);
    }

    public void Add(Course course)
    {
        _context.Courses.Add(course);
    }

    public void Update(Course course)
    {
        _context.Courses.Update(course);
    }

    public void Delete(Course course)
    {
        _context.Courses.Remove(course);
    }
}
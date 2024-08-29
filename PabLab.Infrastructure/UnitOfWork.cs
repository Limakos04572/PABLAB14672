using Microsoft.EntityFrameworkCore;
using PabLab.Domain.Abstractions;
using PabLab.Domain.Entities;
using PabLab.Infrastructure.Context;
using PabLab.Application.Identity.Services;

namespace PabLab.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly PabLabContext _dbContext;
    private readonly UserService _userService;

    public UnitOfWork(PabLabContext dbContext, UserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var auditableEntities = _dbContext
            .ChangeTracker
            .Entries<AuditableEntity>();

        foreach (var entry in auditableEntities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _userService.GetUserName();
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedBy = _userService.GetUserName();
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

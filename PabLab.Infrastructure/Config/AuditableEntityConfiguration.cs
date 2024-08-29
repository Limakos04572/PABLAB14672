using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PabLab.Domain.Entities;

namespace PabLab.Infrastructure.Config;

public abstract class AuditableEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.Property(x => x.CreatedBy)
            .HasMaxLength(400)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(400)
            .IsRequired(false);

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("datetime2(0)")
            .IsRequired(false);
    }
}
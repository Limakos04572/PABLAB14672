using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PabLab.Domain.Entities;

namespace PabLab.Infrastructure.Config;

public class CourseConfiguration : AuditableEntityConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(x => x.CourseId);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Owner)
            .IsRequired();

        base.Configure(builder);
    }
}
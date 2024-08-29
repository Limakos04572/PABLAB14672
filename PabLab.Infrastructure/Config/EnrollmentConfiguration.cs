using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PabLab.Domain.Entities;

namespace PabLab.Infrastructure.Config;

public class EnrollmentConfiguration : AuditableEntityConfiguration<Enrollment>
{
    public override void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(x => x.EnrollmentId);

        builder.Property(x => x.DateEnrollment)
            .IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany(y => y.Enrollments)
            .HasForeignKey(x => x.IdStudent);

        builder.HasOne(x => x.Course)
            .WithMany(y => y.Enrollments)
            .HasForeignKey(x => x.IdCourse);

        base.Configure(builder);
    }
}
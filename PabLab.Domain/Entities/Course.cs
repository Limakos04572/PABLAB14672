namespace PabLab.Domain.Entities;

public class Course : AuditableEntity
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public int Owner { get; set; }

    // Navigation property
    public ICollection<Enrollment> Enrollments { get; set; }
}
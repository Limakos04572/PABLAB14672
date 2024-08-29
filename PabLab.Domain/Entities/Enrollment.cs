namespace PabLab.Domain.Entities;

public class Enrollment : AuditableEntity
{
    public int EnrollmentId { get; set; }
    public int IdStudent { get; set; }
    public int IdCourse { get; set; }
    public DateTime DateEnrollment { get; set; }

    // Navigation properties
    public Student Student { get; set; }
    public Course Course { get; set; }
}
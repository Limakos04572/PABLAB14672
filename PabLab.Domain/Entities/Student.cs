namespace PabLab.Domain.Entities;

public class Student : AuditableEntity
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string Surename { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    // Navigation property
    public ICollection<Enrollment> Enrollments { get; set; }
}
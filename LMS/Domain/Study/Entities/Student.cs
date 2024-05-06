using LMS.Domain.Study.Enums;

namespace LMS.Domain.Study.Entities
{
    public class StudentCourseEntity : BaseAuditableEntity
    {
        public Guid StudentId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid CourseId { get; set; }
        public bool Completed { get; set; }
        public bool Rejected { get; set; }
    }

    public class StudentEntity : BaseAuditableEntity
    {
        public Guid ParentId { get; set; }
        public StudentStatus Status { get; set; }
        public Guid InstitutionMemberId { get; set; }
    }
}

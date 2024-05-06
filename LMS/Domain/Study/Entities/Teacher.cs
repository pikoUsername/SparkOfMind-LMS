namespace LMS.Domain.Study.Entities
{
    public class TeacherEntity : BaseAuditableEntity
    {
        public Guid InstitutionMemberId { get; set; }
        public int Experience { get; set; }
        public SalaryTypes SalaryType { get; set; }
        public DateTime StartDate { get; set; }
        public TeacherStatus Status { get; set; }
        public Guid InstitutionsId { get; set; }
    }
}

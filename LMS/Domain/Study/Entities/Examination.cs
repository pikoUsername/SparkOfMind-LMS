using LMS.Domain.Study.Enums;

namespace LMS.Domain.Study.Entities
{
    public class ExaminationEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ExaminationStatus Status { get; set; }
        public Guid CourseId { get; set; }
        public Guid InstitutionId { get; set; }
    }
}

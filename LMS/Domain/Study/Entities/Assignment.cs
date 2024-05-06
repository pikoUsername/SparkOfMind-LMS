namespace LMS.Domain.Study.Entities
{
    public class AssignmentEntity : BaseAuditableEntity
    {
        public Guid? ExamId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public Guid AttachmentsId { get; set; }
        public Guid CourseId { get; set; }
        public Guid AssignedById { get; set; }
        public Guid AssignedGroupId { get; set; }
    }
}

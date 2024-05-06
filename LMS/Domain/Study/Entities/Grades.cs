namespace LMS.Domain.Study.Entities
{
    public class GradesEntity : BaseAuditableEntity
    {
        public Guid AssignmentId { get; set; }
        public decimal Mark { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public Guid AttachmentsId { get; set; }
    }

    public class GradeTypeEntity : BaseAuditableEntity
    {
        public int? Max { get; set; }
        public int? Min { get; set; }
        public string Variants { get; set; }
        public string Name { get; set; }
    }
}

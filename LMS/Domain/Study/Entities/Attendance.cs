namespace LMS.Domain.Study.Entities
{
    public class AttendanceEntity : BaseAuditableEntity
    {
        public DateTime Date { get; set; }
        public Guid StudentId { get; set; }
        public Guid LessonId { get; set; }
        public string Reason { get; set; }
        public bool Attended { get; set; }
    }
}

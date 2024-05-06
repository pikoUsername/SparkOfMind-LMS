namespace LMS.Domain.Study.Entities
{
    public class DayLessonEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Canceled { get; set; }
        public bool Completed { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GroupId { get; set; }
        public string Room { get; set; }
    }
}

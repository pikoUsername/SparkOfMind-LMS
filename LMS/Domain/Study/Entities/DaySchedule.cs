using LMS.Domain.Study.Enums;

namespace LMS.Domain.Study.Entities
{
    public class DayScheduleEntity : BaseAuditableEntity
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public DayScheduleTypes Type { get; set; }
        public DayOfWeek Day { get; set; }
        public Guid CourseGroupId { get; set; }
        public ICollection<Guid> Lessons { get; set; }
        public string Room { get; set; }
    }
}

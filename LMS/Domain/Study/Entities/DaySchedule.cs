using LMS.Domain.Study.Enums;
using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class DayScheduleEntity : BaseAuditableEntity
    {
        [Required]
        public DateTime StartsAt { get; set; }
        [Required]
        public DateTime EndsAt { get; set; }
        public DayScheduleTypes Type { get; set; } = DayScheduleTypes.Normal; 
        public DayOfWeek Day { get; set; }
        [ForeignKey(nameof(CourseGroupEntity)), Required]
        public Guid CourseGroupId { get; set; }
        public List<DayLessonEntity> Lessons { get; set; } = []; 
        public string? Room { get; set; }

        public static DayScheduleEntity Create(
            DateTime startsAt, DateTime endsAt, DayOfWeek dayofWeek, Guid courseGroupId, string? room = null)
        {
            var day = new DayScheduleEntity()
            {
                CourseGroupId = courseGroupId,
                StartsAt = startsAt,
                EndsAt = endsAt,
                Day = dayofWeek,
                Room = room,
            };

            day.AddDomainEvent(new DayScheduleCreated(day));

            return day; 
        }

        public void AddLessons(params DayLessonEntity[] lessons)
        {
            Lessons.AddRange(lessons); 
        }
    }
}

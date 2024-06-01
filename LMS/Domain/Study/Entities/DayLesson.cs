using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class DayLessonEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [ForeignKey(nameof(CourseEntity)), Required]
        public Guid CourseId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public bool Canceled { get; set; } = false;
        public bool Completed { get; set; } = false;
        [ForeignKey(nameof(TeacherEntity)), Required]
        public Guid TeacherId { get; set; }
        [ForeignKey(nameof(CourseGroupEntity)), Required]
        public Guid GroupId { get; set; }
        [Required]
        public string Room { get; set; } = null!; 

        public static DayLessonEntity Create(
            string name, 
            Guid courseId, 
            DateTime startTime, 
            DateTime endTime, 
            Guid teacherId, 
            Guid groupId, 
            string room)
        {
            var lesson = new DayLessonEntity()
            {
                Name = name, 
                CourseId = courseId,
                StartTime = startTime,
                EndTime = endTime,
                TeacherId = teacherId, 
                GroupId = groupId,
                Room = room
            };

            lesson.AddDomainEvent(new DayLessonCreated(lesson));

            return lesson; 
        }
    }
}

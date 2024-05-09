using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class DayLessonCreated(DayLessonEntity lesson) : DomainEvent
    {
        public DayLessonEntity Lesson { get; set; } = lesson; 
    }
}

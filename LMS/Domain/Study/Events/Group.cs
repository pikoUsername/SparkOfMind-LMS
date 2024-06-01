using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class CourseGroupCreated(CourseGroupEntity group) : DomainEvent
    {
        public CourseGroupEntity Group { get; set; } = group; 
    }
}

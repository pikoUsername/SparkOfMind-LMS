using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class CourseCreated(CourseEntity course) : DomainEvent
    {
        public CourseEntity Course { get; set; } = course; 
    }

    public class CourseClosed(CourseEntity course) : DomainEvent
    {
        public CourseEntity Course { get; set; } = course; 
    }
}

using LMS.Domain.Payment.Entities;
using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class StudentCourseCreated(StudentCourseEntity student) : DomainEvent
    {
        public StudentCourseEntity StudentCourse { get; set; } = student; 
    }

    public class StudentCreated(StudentEntity student) : DomainEvent
    {
        public StudentEntity Student { get; set; } = student; 
    }
}

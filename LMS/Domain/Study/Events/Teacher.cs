using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class TeacherCreated(TeacherEntity teacher) : DomainEvent 
    {
        public TeacherEntity Teacher { get; set; } = teacher; 
    }
}

using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class GradeCreated(GradeEntity grade) : DomainEvent 
    {
        public GradeEntity Grade { get; set; } = grade; 
    }
}

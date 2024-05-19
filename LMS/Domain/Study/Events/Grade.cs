using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class GradeCreated(SubmissionEntity grade) : DomainEvent 
    {
        public SubmissionEntity Grade { get; set; } = grade; 
    }
}

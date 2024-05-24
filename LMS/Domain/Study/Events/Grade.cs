using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class SubmissionCreated(SubmissionEntity grade) : DomainEvent 
    {
        public SubmissionEntity Grade { get; set; } = grade; 
    }
}

using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class SubmissionMarked(SubmissionEntity entity) : DomainEvent
    {
        public SubmissionEntity Submission { get; } = entity; 
    }
}

using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class AssignmentCreated(AssignmentEntity assignment) : DomainEvent 
    {
        public AssignmentEntity Assignment { get; set; } = assignment;
    }
}

using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class InvitationCreated(Guid userId, InvitationEntity invitation) : DomainEvent
    {
        public Guid UserId = userId;
        public InvitationEntity Invitation = invitation; 
    }
}

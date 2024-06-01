using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InvitationEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(UserEntity))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(InstitutionEntity))]
        public Guid InstiutionId { get; set; }
        public bool Completed { get; set; } = false;
        public bool IsTeacher { get; set; } = false; 

        public void Complete()
        {
            Completed = true;
        }

        public static InvitationEntity Create(Guid userId, Guid institutionId, bool isTeacher)
        {
            var invitation = new InvitationEntity
            {
                UserId = userId,
                InstiutionId = institutionId,
                IsTeacher = isTeacher,
            };

            invitation.AddDomainEvent(new InvitationCreated(userId, invitation));

            return invitation; 
        }
    }
}

using LMS.Application.Study.EventHandlers;
using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Enums;
using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        // email should be verfified before setting up
        public string Email { get; private set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public InstitutionTypes Type { get; set; } = InstitutionTypes.EduCenter;
        [Required]
        public InstitutionStatus Status { get; set; } = InstitutionStatus.Active; 
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public ICollection<FileEntity> Images { get; set; } = [];
        [Required, ForeignKey(nameof(UserEntity))]
        public Guid OwnerId { get; init; }  // not allowed to change owner of institution!!! 
        public decimal Fee { get; private set; } = 0;
        // marked for deletion 
        public bool Deleted { get; private set; } = false;
        public bool Blocked { get; private set; } = false;
        public string? BlockedReason { get; private set; } 

        public static InstitutionEntity Create(
            string name, 
            string description, 
            string email, 
            string phone, 
            string address, 
            Guid ownerId)
        {
            var institution = new InstitutionEntity()
            {
                Name = name,
                Phone = phone,
                Address = address,
                OwnerId = ownerId,
                Description = description,
                Email = email,
            };

            institution.AddDomainEvent(new InstitutionCreated(institution));

            return institution; 
        }

        public void ChangeFee(decimal fee)
        {
            var oldfee = Fee; 
            Fee = fee;

            AddDomainEvent(new InstitutionFeeChanged(this, fee, oldfee)); 
        }

        public void ChangeEmail(string email)
        {
            Email = email;

            AddDomainEvent(new InstitutionEmailChanged(this));
        }

        public void Block(string reason, UserEntity blockedBy)
        {
            if (Blocked || Deleted)
                throw new InvalidOperationException("Institution is already deleted or blocked");
            Blocked = true;
            BlockedReason = reason;
            AddDomainEvent(new InstitutionBlocked(this, reason, blockedBy)); 
        }

        public void MarkDelete(string reason, UserEntity deletedBy)
        {
            Deleted = true;
            BlockedReason = reason;
            AddDomainEvent(new InstitutionMarkedDeleted(this, reason, deletedBy));
        }
    }
}

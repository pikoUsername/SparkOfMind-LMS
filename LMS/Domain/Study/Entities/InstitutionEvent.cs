using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionEventEntity : BaseAuditableEntity
    {
        [Required, ForeignKey(nameof(InstitutionEntity))]
        public Guid InstitutionId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public ICollection<InstitutionRolesEntity> Roles { get; set; } = [];
        [ForeignKey(nameof(UserEntity)), Required]
        // use this one, not CreatedById
        public UserEntity CreatedBy { get; set; } = null!; 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public static InstitutionEventEntity Create(
            string title, 
            string text, 
            Guid institutionId,
            UserEntity createdBy, 
            DateTime? startDate = null, 
            DateTime? endDate = null)
        {
            var @event = new InstitutionEventEntity() { 
                Title = title, 
                Text = text,
                InstitutionId = institutionId,
                CreatedBy = createdBy, 
                StartDate = startDate,
                EndDate = endDate
            };

            @event.AddDomainEvent(new InstitutionEventCreated(@event)); 

            return @event;
        }
    }
}

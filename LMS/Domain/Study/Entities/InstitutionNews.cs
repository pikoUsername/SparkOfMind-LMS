using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionNewsEntity : BaseAuditableEntity
    {
        [Required]
        public string Title { get; set; } = null!; 
        [Required]
        public string AllowedToSee { get; set; } = null!; 
        [Required, ForeignKey(nameof(InstitutionEntity))]
        public Guid InstitutionId { get; set; }
        [Required]
        public string Text { get; set; } = null!;
        public ICollection<FileEntity> Attachments { get; set; } = []; 

        public static InstitutionNewsEntity Create(string title, string allowedToSee, string text, Guid institutionId, ICollection<FileEntity> attachments)
        {
            var result = new InstitutionNewsEntity()
            {
                Title = title,
                AllowedToSee = allowedToSee,
                Text = text,
                InstitutionId = institutionId,
                Attachments = attachments, 
            }; 

            result.AddDomainEvent(new NewEntityCreated(result));

            return result; 
        }
    }
}

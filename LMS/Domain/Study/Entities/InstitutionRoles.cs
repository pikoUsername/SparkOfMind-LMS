using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionRolesEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public ICollection<PermissionEntity> Permissions { get; set; } = [];
        [ForeignKey(nameof(InstitutionEntity))]
        public Guid InstitutionId { get; set; }

        public static InstitutionRolesEntity Create()
        {
            var result = new InstitutionRolesEntity();

            result.AddDomainEvent(new InstitutionRoleCreated(result));

            return result; 
        }
    }
}

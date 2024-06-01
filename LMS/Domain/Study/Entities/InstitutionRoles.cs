using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionRolesEntity : BaseAuditableEntity
    {
        public static List<string> AllowedSubjectNames = [nameof(StudentEntity)]; 

        [Required]
        public string Name { get; private set; } = null!;
        [Required]
        public List<PermissionEntity> Permissions { get; private set; } = [];
        [ForeignKey(nameof(InstitutionEntity))]
        public Guid InstitutionId { get; init; }

        public static InstitutionRolesEntity Create(Guid institutionId, string name)
        {
            var result = new InstitutionRolesEntity() { 
                InstitutionId = institutionId, 
                Name = name
            };

            result.AddDomainEvent(new InstitutionRoleCreated(result));

            return result; 
        }

        public void AddPermission(PermissionEntity permission, InstitutionEntity institution)
        {
            if (!AllowedSubjectNames.Any(x => x == permission.SubjectName))
            {
                throw new AccessDenied("You are trying to add permission outside of institution scope!"); 
            }
            Permissions.Add(permission);

            AddDomainEvent(new InstitutionRolePermissionAdded(institution, this)); 
        }
    }
}

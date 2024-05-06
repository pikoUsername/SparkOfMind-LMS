using LMS.Domain.User.Events;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LMS.Domain.User.Entities
{
    public class PermissionEntity : BaseAuditableEntity
    {
        [Required]
        public string SubjectName { get; set; } = null!;
        [Required]
        public string SubjectAction { get; set; } = null!;
        [Required]
        public string SubjectId { get; set; } = null!;

        public static PermissionEntity Create(string Name, string Action, string subjectId)
        {
            var perm = new PermissionEntity
            {
                SubjectName = Name,
                SubjectAction = Action,
                SubjectId = subjectId
            };

            perm.AddDomainEvent(new PermissionCreated(perm));

            return perm;
        }

        public string Join()
        {
            return $"{SubjectName}:{SubjectId}:{SubjectAction}"; 
        }
    }
}

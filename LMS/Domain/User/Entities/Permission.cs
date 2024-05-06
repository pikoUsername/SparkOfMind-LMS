using LMS.Domain.User.Events;
using LMS.Domain.User.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.User.Entities
{
    public class PermissionEntity : BaseAuditableEntity, IPermissionEntity
    {
        [Required]
        public string SubjectName { get; set; } = null!;
        [Required]
        public string SubjectAction { get; set; } = null!;
        [Required]
        public string SubjectId { get; set; } = null!;

        public static PermissionEntity Create(string name, string subjectId, string action)
        {
            var perm = new PermissionEntity
            {
                SubjectName = name,
                SubjectAction = action,
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

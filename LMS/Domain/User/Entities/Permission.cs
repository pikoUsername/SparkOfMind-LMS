using LMS.Domain.User.Enums;
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
        // ["write", "read", "edit", "delete", "extend", "*"]
        public List<string> SubjectActions { get; set; } = [];
        [Required]
        public string SubjectId { get; set; } = null!;

        public static PermissionEntity Create(string subjectName, Guid subjectId, PermissionEnum action)
        {
            string actionAsString; 

            if (action == PermissionEnum.all)
            {
                actionAsString = "*"; 
            } else
            {
                actionAsString = action.ToString();
            }

            var perm = new PermissionEntity
            {
                SubjectName = subjectName,
                SubjectActions = [actionAsString],
                SubjectId = subjectId.ToString()
            };

            perm.AddDomainEvent(new PermissionCreated(perm));

            return perm;
        }

        public static PermissionEntity Create(string subjectName, string subjectId, PermissionEnum action)
        {
            var perm = new PermissionEntity
            {
                SubjectName = subjectName,
                SubjectActions = [action.ToString()],
                SubjectId = subjectId, 
            };

            perm.AddDomainEvent(new PermissionCreated(perm));

            return perm;
        }

        public static PermissionEntity[] CreateList(string subjectName, string subjectId, params PermissionEnum[] actions)
        {
            List<PermissionEntity> perms = []; 
            foreach (var action in actions)
            {
                var perm = Create(subjectName, subjectId, action);

                perms.Add(perm);
            }

            return perms.ToArray();
        }

        public string[] Join()
        {
            string[] result = []; 
            foreach (var action in SubjectActions)
            {
                result.Append($"{SubjectName}:{SubjectId}:{action}");
            }
            return result; 
        }
    }
}

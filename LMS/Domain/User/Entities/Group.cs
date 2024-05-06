using LMS.Domain.User.Events;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.User.Entities
{
    public class GroupEntity : BaseAuditableEntity
    {
        [Required]
        public ICollection<PermissionEntity> Permissions { get; set; } = [];
        [Required]
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public ICollection<UserEntity> Users { get; set; } = [];

        // Repeating code! 
        public void AddPermission(PermissionEntity permission)
        {
            Permissions.Add(permission);

            AddDomainEvent(new PermissionAdded("GROUP", permission));
        }

        public void AddPermissionWithCode(string subjectName, string subjectId, params string[] actions)
        {
            foreach (var action in actions)
            {
                AddPermission(PermissionEntity.Create(subjectName, subjectId, action));
            }
        }
    }
}

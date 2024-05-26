using LMS.Domain.User.Events;
using LMS.Domain.User.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.User.Entities
{
    public class GroupEntity : BaseAuditableEntity
    {
        [Required]
        public PermissionCollection Permissions { get; set; } = [];
        [Required]
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public ICollection<UserEntity> Users { get; set; } = [];

        public static GroupEntity Create(string name)
        {
            var group = new GroupEntity()
            {
                Name = name,
                Users = [],
                Permissions = [],
            };

            group.AddDomainEvent(new GroupCreated(group));

            return group; 
        }

        public void AddUser(UserEntity user)
        {
            Users.Add(user);

            AddDomainEvent(new GroupUserAdded(this, user)); 
        }
    }
}

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
    }
}

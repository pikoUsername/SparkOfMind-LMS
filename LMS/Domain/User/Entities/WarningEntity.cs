using LMS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.User.Entities
{
    public class WarningEntity : BaseAuditableEntity
    {
        public string Reason { get; set; } = null!;
        [ForeignKey(nameof(UserEntity))]
        public Guid ByUserId { get; set; }
    }
}

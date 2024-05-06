using LMS.Domain.Study.Enums;

namespace LMS.Domain.Study.Entities
{
    public class InstitutionEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public InstitutionTypes Type { get; set; }
        public InstitutionStatus Status { get; set; }
        public string Description { get; set; }
        public Guid ImagesId { get; set; }
        public Guid OwnerId { get; set; }
        public decimal Fee { get; set; }
    }
}

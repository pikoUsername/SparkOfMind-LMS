namespace LMS.Domain.Study.Entities
{
    public class InstitutionMemberEntity : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid RolesId { get; set; }
        public Guid InstitutionId { get; set; }
    }
}

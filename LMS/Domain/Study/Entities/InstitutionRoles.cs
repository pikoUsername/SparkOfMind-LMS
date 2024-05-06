namespace LMS.Domain.Study.Entities
{
    public class InstitutionRolesEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Guid InstitutionId { get; set; }
    }
}

namespace LMS.Domain.Study.Entities
{
    public class InstitutionEventEntity : BaseAuditableEntity
    {
        public Guid InstitutionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string AllowedToSee { get; set; }
        public string Role { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

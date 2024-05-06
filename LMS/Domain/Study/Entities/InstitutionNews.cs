namespace LMS.Domain.Study.Entities
{
    public class InstitutionNewsEntity : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string AllowedToSee { get; set; }
        public Guid InstitutionId { get; set; }
        public string Text { get; set; }
        public Guid AttachmentsId { get; set; }
    }
}

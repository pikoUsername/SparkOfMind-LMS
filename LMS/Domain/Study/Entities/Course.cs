namespace LMS.Domain.Study.Entities
{
    public class CourseEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndsAt { get; set; }
        public int Hours { get; set; }
        public DateTime? StartsAt { get; set; }
        public Guid InstitutionId { get; set; }
        public ICollection<Guid> Teachers { get; set; }
        public Guid CategoryId { get; set; }
        public bool Closed { get; set; }
        public string Attributes { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}

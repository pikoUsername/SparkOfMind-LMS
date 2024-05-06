namespace LMS.Domain.Study.Entities
{
    public class CategoryEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> Attributes { get; set; }
    }

    public class AttributesEntity : BaseAuditableEntity
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }
}

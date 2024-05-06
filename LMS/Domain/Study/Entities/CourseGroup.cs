namespace LMS.Domain.Study.Entities
{
    public class CourseGroupEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Guid InstitutionId { get; set; }
        public ICollection<Guid> Students { get; set; }
        public Guid GradeTypeId { get; set; }
    }
}

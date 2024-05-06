namespace LMS.Domain.Study.Entities
{
    public class BooksEntity : BaseAuditableEntity
    {
        public Guid InstitutionId { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOnline { get; set; }
        public string Link { get; set; }
    }

    public class BooksRentEntity : BaseAuditableEntity
    {
        public Guid BookId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Notified { get; set; }
        public bool Expired { get; set; }
        public DateTime? PassedTime { get; set; }
    }
}

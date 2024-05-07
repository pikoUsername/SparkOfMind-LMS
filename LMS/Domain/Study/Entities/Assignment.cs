using LMS.Domain.Files.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Study.Entities
{
    public class AssignmentEntity : BaseAuditableEntity
    {

        public Guid? ExamId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = null!; 
        public bool Required { get; set; }
        public ICollection<FileEntity> Attachments { get; set; } = []; 
        public Guid CourseId { get; set; }
        public Guid AssignedById { get; set; }
        public Guid AssignedGroupId { get; set; }
    }
}

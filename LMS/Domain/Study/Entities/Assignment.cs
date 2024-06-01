using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LMS.Domain.Study.Entities
{
    public class AssignmentEntity : BaseAuditableEntity
    {
        public ExaminationEntity? Examination { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        public bool Required { get; set; } = false;
        public GradeTypeEntity GradeType { get; set; } = null!; 
        public ICollection<FileEntity> Attachments { get; set; } = []; 
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public TeacherEntity AssignedBy { get; set; } = null!; 
        [Required]
        public Guid AssignedGroupId { get; set; }
        [Required]
        public Guid InstitutionId { get; set; }

        public static AssignmentEntity Create(
            string name, 
            DateTime startDate, 
            DateTime endDate, 
            string description, 
            Guid courseId, 
            TeacherEntity assginedBy, 
            CourseGroupEntity courseGroup, 
            ExaminationEntity? examination = null)
        {
            var assignment = new AssignmentEntity
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Description = description,
                CourseId = courseId,
                AssignedBy = assginedBy,
                AssignedGroupId = courseGroup.Id,
                GradeType = courseGroup.GradeType, 
                InstitutionId = assginedBy.InstitutionId, 
                Examination = examination
            };
            assignment.AddDomainEvent(new AssignmentCreated(assignment));

            return assignment;
        }
    }
}

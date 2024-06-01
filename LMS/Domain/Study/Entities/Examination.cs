using LMS.Domain.Study.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class ExaminationEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public ExaminationStatus Status { get; set; }
        [ForeignKey(nameof(CourseEntity)), Required]
        public Guid CourseId { get; set; }
        [ForeignKey(nameof(InstitutionEntity)), Required]
        public Guid InstitutionId { get; set; }

        public static ExaminationEntity Create(string name, ExaminationStatus status, Guid courseId, Guid institutionId)
        {
            var examination = new ExaminationEntity
            {
                Name = name,
                Status = status,
                CourseId = courseId,
                InstitutionId = institutionId
            }; 

            return examination;
        }
    }
}

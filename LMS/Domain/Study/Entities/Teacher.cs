using LMS.Domain.Study.Enums;
using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class TeacherEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(InstitutionMemberEntity)), Required]
        public Guid InstitutionMemberId { get; set; }
        public int? Experience { get; set; }
        public SalaryType? SalaryType { get; set; }
        public DateTime? StartDate { get; set; }
        [Required]
        public TeacherStatus Status { get; set; }
        [ForeignKey(nameof(InstitutionEntity)), Required]
        public Guid InstitutionId { get; set; }

        public static TeacherEntity Create(Guid memberId, Guid institutionId, TeacherStatus status)
        {
            var teacher = new TeacherEntity() { 
                InstitutionMemberId = memberId,
                InstitutionId = institutionId,
                Status = status
            };

            teacher.AddDomainEvent(new TeacherCreated(teacher));

            return teacher; 
        }
    }
}

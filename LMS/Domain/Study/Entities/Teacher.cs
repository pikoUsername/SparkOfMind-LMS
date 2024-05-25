using LMS.Domain.Study.Enums;
using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class TeacherEntity : BaseAuditableEntity
    {
        [Required]
        public InstitutionMemberEntity InstitutionMember { get; set; } = null!; 
        public int? Experience { get; set; }
        public SalaryType? SalaryType { get; set; }
        public DateTime? StartDate { get; set; }
        [Required]
        public TeacherStatus Status { get; set; }
        [ForeignKey(nameof(InstitutionEntity)), Required]
        public Guid InstitutionId { get; set; }
        public UserEntity User { get; set; } = null!; 

        public static TeacherEntity Create(Guid institutionId, UserEntity user, TeacherStatus status)
        {
            var teacher = new TeacherEntity() {
                InstitutionMember = InstitutionMemberEntity.Create(user.Id, institutionId), 
                Teacher = user, 
                InstitutionId = institutionId,
                Status = status
            };

            teacher.AddDomainEvent(new TeacherCreated(teacher));

            return teacher; 
        }
    }
}

using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using LMS.Domain.Study.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Study.Entities
{
    public class SubmissionEntity : BaseAuditableEntity
    {
        [Required, ForeignKey(nameof(AssignmentEntity))]
        public Guid AssignmentId { get; set; }
        [Required]
        public string Mark { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Comment { get; set; } = null!;
        [Required, ForeignKey(nameof(StudentEntity))]
        public Guid StudentId { get; set; }
        public ICollection<FileEntity> Attachments { get; set; } = [];

        public static SubmissionEntity Create(
            AssignmentEntity assigment,
            string mark,
            string comment,
            Guid studentId,
            ICollection<FileEntity> attachments)
        {
            // throws exception if not valid mark
            AssigmentService.VerifyMark(mark, assigment.GradeType);
            var grade = new SubmissionEntity()
            {
                AssignmentId = assigment.Id,
                Mark = mark,
                Comment = comment,
                StudentId = studentId,
                Attachments = attachments,
                Date = DateTime.UtcNow
            };

            grade.AddDomainEvent(new GradeCreated(grade));

            return grade;
        }
    }
}

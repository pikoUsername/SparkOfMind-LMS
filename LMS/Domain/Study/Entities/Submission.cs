using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using LMS.Domain.Study.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Study.Entities
{
    public class SubmissionEntity : BaseAuditableEntity
    {
        [Required]
        public AssignmentEntity Assignment { get; set; } = null!; 
        public string? Mark { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        public string? TeacherComment { get; set; } = null!;
        public string? Comment { get; set; } = null!; 
        [Required, ForeignKey(nameof(StudentEntity))]
        public Guid StudentId { get; set; }
        public ICollection<FileEntity> Attachments { get; set; } = [];
        public bool AllowedToEditByStudent { get; set; } = false; 

        public static SubmissionEntity Create(
            AssignmentEntity assignment,
            string? comment,
            Guid studentId,
            ICollection<FileEntity> attachments)
        {
            // throws exception if not valid mark

            var submission = new SubmissionEntity()
            {
                Assignment = assignment,
                Comment = comment,
                StudentId = studentId,
                Attachments = attachments,
                Date = DateTime.UtcNow
            };

            submission.AddDomainEvent(new SubmissionCreated(submission));

            return submission;
        }

        public void MarkSubmission(string mark, AssignmentEntity assignment)
        {
            AssignmentService.VerifyMark(mark, assignment.GradeType);

            Mark = mark; 

            AddDomainEvent(new SubmissionMarked(this));     
        }

        
    }
}

using LMS.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class GetSubmissionDto : InputInstitution
    {
        [Required]
        public Guid SubmissionId { get; set; }
    }

    public class GetSubmissionsListDto : InputInstitution
    {
        [Required]
        public Guid AssignmentId { get; set; }
    }

    public class UpdateSubmissionByStudentDto : InputInstitution
    {
        [Required]
        public Guid AssignmentId { get; set; }
        public List<CreateFileDto>? Attachments { get; set; }
        [Required]
        public string? Comment { get; set; }
    }
}

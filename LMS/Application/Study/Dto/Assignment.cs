using LMS.Application.Files.Dto;
using LMS.Domain.Study.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CompleteAssignmentDto : InputInstitution 
    {
        [Required]
        public Guid AssignmentId { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;
        public ICollection<CreateFileDto>? Files { get; set; }
    }

    public class CreateAssignmentDto : InputInstitution
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        public bool Required { get; set; } = false;
        public bool IsScheduled { get; set; } = false; 
        public GradeTypeEntity? GradeType { get; set; } 
        public ICollection<CreateFileDto> Attachments { get; set; } = [];
        [Required]
        public Guid AssignedGroupId { get; set; }
        public Guid? ExaminationId { get; set; }
    }

    public class UpdateAssignmentDto : InputInstitution
    {
        [Required]
        public Guid AssignmentId { get; set; }
        public string? Description { get; set; } 
        public List<CreateFileDto>? Attachments { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }   
        public Guid? GradeType { get; set; }
    }

    public class GetAssignmentDto : InputInstitution {
        [Required]
        public Guid AssignmentId { get; set; }
    }

    public class GetAssignmentListDto : InputInstitution {
        [Required]
        public Guid GroupCourseId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
    }

    public class DeleteAssignmentDto : InputInstitution {
        [Required]
        public Guid AssignmentId { get; set; }
    }
}

using LMS.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CompleteAssigmentDto
    {
        [Required]
        public Guid AssignmentId { get; set; }
        [Required]
        public Guid InstitutionId { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;
        public ICollection<CreateFileDto>? Files { get; set; }
    }
}

using LMS.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace LMS.Presentation.Web.Schemas
{
    public class CreateCommentScheme
    {
        [Required]
        public string Text { get; set; } = null!;
        public Guid? ParentCommentId { get; set; }
        public ICollection<CreateFileDto> Files { get; set; } = [];
    }
}

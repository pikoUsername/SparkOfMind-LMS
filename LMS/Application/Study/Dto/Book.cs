using LMS.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        public bool IsOnline { get; set; } = false;
        [Required]
        public Guid InstitutionId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Link { get; set; }
    }

    public class GetBookDto {
        [Required]
        public Guid InstitutionId { get; set; }
        public Guid BookId { get; set; }
        // hmm why? 
        public string? Link { get; set; }
    }

    public class GetBooksListDto : InputPagination
    {
        [Required]
        public Guid InstitutionId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Name { get; set; }
        public bool? IsOnline { get; set; }
    }

    public class RentBookDto : InputInstitution
    {
        public Guid StudentId;
        public Guid BookId;
        public DateTime EndTime; 
    }

    public class PassBookDto : InputInstitution
    {
        public Guid BookId;
        public Guid StudentId; 
    }

    public class DeleteBookDto {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Guid InstitutionId { get; set; }
    }
}

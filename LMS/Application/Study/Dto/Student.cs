using LMS.Domain.Study.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CreateStudentDto
    {
        [Required]
        public Guid PurchaseId { get; set; }
        [Required]
        public Guid InstitutionId { get; set; }
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public GenderTypes Gender { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public int? Age { get; set; }
        [Required, Phone]
        public string Phone { get; set; } = null!; 
    }

    public class GetStudentDto
    {
        public Guid? StudentId { get; set; }
        public string? Fullname { get; set; } 
        public string? Phone { get; set; }
    }

    public class UpdateStudentDto : InputInstitution
    {
        public Guid StudentId;
        public string? Address;
        public DateTime? BirthDate;
        [Phone]
        public string? Phone; 
    }
}

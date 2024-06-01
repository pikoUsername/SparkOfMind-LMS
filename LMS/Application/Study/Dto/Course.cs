using LMS.Application.Common.Models;
using LMS.Domain.Payment.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CreateCourseDto : InputInstitution
    {
        public string Name = null!;
        public Guid CategoryId; 
        public string Description = null!;
        public int Hours;
        public ICollection<Guid> TeacherIds = [];
        public Dictionary<string, string> Attributes = null!;
        public Money BasePrice = null!;
        public DateTime? StartsAt;
        public DateTime EndsAt;
    }

    public class UpdateCourseDto : InputInstitution
    {
        public Guid CourseId; 
        public string? Name;
        public string? Description = null!;
        public int? Hours;
        public ICollection<Guid>? TeacherIds = [];
        public Dictionary<string, string>? Attributes = null!;
        public Money? BasePrice;
        public Money? DiscountPrice; 
        public DateTime? StartsAt;
    }

    public class CloseCourseDto : InputInstitution
    {
        public Guid CourseId;
    }

    public class GetCourseDto {
        public Guid CourseId; 
    }

    public class GetCoursesListDto : InputPagination
    {
        public string? Name;
        public Guid? CategoryId;
        public Guid? InstitutionId;
        public decimal? StartingPrice; 
        public decimal? EndingPrice;
    }
}

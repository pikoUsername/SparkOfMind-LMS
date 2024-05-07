using LMS.Domain.Payment.ValueObjects;
using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class CourseEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!; 
        public DateTime EndsAt { get; set; }
        public int Hours { get; set; }
        public DateTime? StartsAt { get; set; }
        public InstitutionEntity Institution { get; set; } = null!; 
        public ICollection<TeacherEntity> Teachers { get; set; } = [];
        public CategoryEntity Category { get; set; } = null!; 
        public bool Closed { get; set; } = false;
        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = null!;
        [Required]
        public Money BasePrice { get; set; } = null!; 
        public Money? DiscountPrice { get; set; }

        [NotMapped, Required]
        public Money CurrentPrice
        {
            get
            {
                if (DiscountPrice is null)
                {
                    return BasePrice;
                }
                else
                {
                    return DiscountPrice;
                }
            }
        }

        public static CourseEntity Create(
            string name, 
            string description, 
            int hours, 
            InstitutionEntity institution, 
            CategoryEntity category, 
            Money price)
        {
            var course = new CourseEntity()
            {
                Name = name, 
                Description = description, 
                Hours = hours,
                Institution = institution,
                Category = category,
                BasePrice = price, 
            };

            course.AddDomainEvent(new CourseCreated(course)); 

            return course; 
        }
    }
}

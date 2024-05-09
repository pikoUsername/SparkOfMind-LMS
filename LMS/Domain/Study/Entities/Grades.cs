using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using LMS.Domain.Study.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LMS.Domain.Study.Entities
{
    public class GradeEntity : BaseAuditableEntity
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

        public static GradeEntity Create(
            AssignmentEntity assigment, 
            string mark, 
            string comment, 
            Guid studentId, 
            ICollection<FileEntity> attachments)
        {
            // throws exception if not valid mark
            AssigmentService.VerifyMark(mark, assigment.GradeType); 
            var grade = new GradeEntity()
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

    public class GradeTypeEntity : BaseAuditableEntity
    {
        public int? Max { get; set; }
        public int? Min { get; set; }
        [Column(TypeName = "jsonb")]
        public string? Variants { get; set; }
        [Required]
        public string Name { get; set; } = null!; 
    
        public static GradeTypeEntity CreateVariants(string name, List<string> variants)
        {
            var variantsAsStr = JsonSerializer.Serialize(variants);

            var gradeType = new GradeTypeEntity()
            {
                Name = name,
                Variants = variantsAsStr,
            };

            return gradeType; 
        }

        public static GradeTypeEntity CreateBologna(string name)
        {
            return new GradeTypeEntity()
            {
                Name = name,
                Min = 0,
                Max = 100,
            }; 
        }

        public static GradeTypeEntity Create(string name, int min, int max)
        {
            return new GradeTypeEntity()
            {
                Name = name,
                Min = min,
                Max = max,
            }; 
        }
    }
}

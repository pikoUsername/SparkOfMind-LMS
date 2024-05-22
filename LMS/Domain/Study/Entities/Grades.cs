using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Events;
using LMS.Domain.Study.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LMS.Domain.Study.Entities
{
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

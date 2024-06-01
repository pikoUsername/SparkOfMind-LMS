using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LMS.Domain.Study.Entities
{
    public class CategoryEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Slug { get; set; } = null!;
        [Column(TypeName = "jsonb")]
        public string Tags { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!; 
        public ICollection<AttributeEntity> Attributes { get; set; } = []; 

        public static CategoryEntity Create(
            string name, 
            string tags, 
            string description, 
            ICollection<AttributeEntity> attributes)
        {
            var category = new CategoryEntity()
            {
                Name = name, 
                Tags = tags,
                Description = description,
                Attributes = attributes
            };

            category.AddDomainEvent(new CategoryCreated(category)); 

            return category; 
        }

        public void AddTags(params string[] tags)
        {
            if (!string.IsNullOrEmpty(Tags))
                Tags = "[]"; 
            var categoryTags = JsonSerializer.Deserialize<string[]>(Tags); 
            var newTags = new List<string>();
            if (categoryTags != null)
                newTags.AddRange(categoryTags);
            newTags.AddRange(tags); 
            Tags = JsonSerializer.Serialize(newTags);
        }
    }

    public class AttributeEntity : BaseAuditableEntity
    {
        [Required]
        public string Label { get; set; } = null!;
        [Required]
        public string Value { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!; 

        public static AttributeEntity Create(string name, string value, string label)
        {
            return new AttributeEntity()
            {
                Name = name,
                Value = value,
                Label = label
            }; 
        }
    }
}

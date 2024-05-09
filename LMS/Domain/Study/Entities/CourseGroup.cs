using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class CourseGroupEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required, ForeignKey(nameof(InstitutionEntity))]
        public Guid InstitutionId { get; set; }
        [Required]
        public List<StudentEntity> Students { get; private set; } = [];
        public GradeTypeEntity? GradeType { get; set; } 
        // groups could be mixed! 
        public Guid? CourseId { get; set; }

        public static CourseGroupEntity Create(string name, Guid institutionId, GradeTypeEntity? gradeType = null)
        {
            var courseGroup = new CourseGroupEntity() {
                Name = name,
                InstitutionId = institutionId,
                Students = [],
                GradeType = gradeType
            };

            courseGroup.AddDomainEvent(new CourseGroupCreated(courseGroup));

            return courseGroup; 
        }

        public void AddStudents(params StudentEntity[] students)
        {
            if (students.Any(x => x.InstitutionMember.InstitutionId != InstitutionId))
            {
                throw new Exception("Student is not inside of one institution and group"); 
            }
            Students.AddRange(students); 
        }

        public void DeleteStudent(Guid studentId)
        {
            var student = Students.Find(x => x.Id == studentId); 
            if (student != null) 
                Students.Remove(student);
        }
    }
}

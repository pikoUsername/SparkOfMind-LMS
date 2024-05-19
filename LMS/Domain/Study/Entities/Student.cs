using LMS.Domain.Payment.Entities;
using LMS.Domain.Study.Enums;
using LMS.Domain.Study.Events;
using LMS.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class StudentCourseEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(StudentEntity)), Required]
        public Guid StudentId { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }
        [Required, ForeignKey(nameof(PurchaseEntity))]
        public Guid PurchaseId { get; set; }
        [Required, ForeignKey(nameof(CourseEntity))]
        public Guid CourseId { get; set; }
        [Required]
        public bool Completed { get; set; } = false;
        [Required]
        public bool Rejected { get; set; } = false; 

        public static StudentCourseEntity Create(Guid studentId, DateTime admissionDate, Guid purchaseId, Guid courseId)
        {
            var studentCourse = new StudentCourseEntity()
            {
                StudentId = studentId,
                AdmissionDate = admissionDate,
                PurchaseId = purchaseId,
                CourseId = courseId,
                Completed = false
            }; 

            studentCourse.AddDomainEvent(new StudentCourseCreated(studentCourse));

            return studentCourse;   
        }
    }

    public class StudentEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(UserEntity)), Required]
        public Guid ParentId { get; set; }
        public string FullName { get; set; } = null!; 
        public string? Phone { get; set; } 
        [Required]
        public StudentStatus Status { get; set; }
        [Required]
        public InstitutionMemberEntity InstitutionMember { get; set; } = null!; 
        public DateOnly? BirthDate { get; set; }
        [Required]
        public string? Address { get; set; } = null!;
        public ICollection<StudentCourseEntity> Courses { get; set; } = [];
        public UserEntity User { get; set; } = null!; 

        public static StudentEntity CreateFromUser(UserEntity user, UserEntity parent, Guid institutionId, StudentStatus status)
        {
            if (string.IsNullOrEmpty(parent.Phone)) {
                throw new ArgumentException($"{parent.Id} does not have bounded phone!"); 
            }
            var member = InstitutionMemberEntity.Create(user.Id, institutionId);

            var student = new StudentEntity() { 
                InstitutionMember = member, 
                Phone = user.Phone, 
                Status = status,
                FullName = user.Fullname, 
                User = user, 
                ParentId = parent.Id,
            };

            student.AddDomainEvent(new StudentCreated(student));
            
            return student;
        }
    }
}

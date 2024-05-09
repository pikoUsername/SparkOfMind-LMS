using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class BookEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(InstitutionEntity)), Required]
        public Guid InstitutionId { get; set; }
        [ForeignKey(nameof(CourseEntity)), Required]
        public Guid CourseId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public bool IsOnline { get; set; } = false; 
        public string? Link { get; set; } = null!;
        
        public static BookEntity Create(Guid institutionId, Guid courseId, string name, string description)
        {
            var book = new BookEntity()
            {
                InstitutionId = institutionId,
                CourseId = courseId,
                Name = name,
                Description = description
            };

            book.AddDomainEvent(new BookCreated(book));

            return book; 
        }    

        public static BookEntity CreateOnline(Guid institutionId, Guid courseId, string name, string description, string link)
        {
            var book = new BookEntity()
            {
                InstitutionId = institutionId,
                CourseId = courseId,
                Name = name,
                Description = description, 
                IsOnline = true, 
                Link = link
            };

            book.AddDomainEvent(new BookCreated(book));

            return book;

        }
    }

    public class BookRentEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(BookEntity)), Required]
        public Guid BookId { get; set; }
        [ForeignKey(nameof(StudentEntity)), Required]
        public Guid StudentId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool Notified { get; set; } = false;
        public bool Expired { get; set; } = false; 
        public DateTime? PassedTime { get; set; } 

        public static BookRentEntity Create(BookEntity book, Guid studentId, DateTime startDate, DateTime endDate)
        {
            if (book.IsOnline)
            {
                throw new Exception("Cannot register online book"); 
            }
            var bookRent = new BookRentEntity()
            {
                BookId = book.Id,
                StudentId = studentId,
                StartDate = startDate,
                EndDate = endDate,
            };

            bookRent.AddDomainEvent(new BookRentCreated(bookRent));

            return bookRent; 
        }

        public void Notifiy()
        {
            Notified = true;

            AddDomainEvent(new BookRentNotified(this)); 
        }

        public void PassBook(BookEntity book, Guid studentId)
        {
            if (studentId == StudentId)
                throw new Exception("Student is not equal to student that has passed this book"); 
            PassedTime = DateTime.UtcNow; 
            if (PassedTime > EndDate)
            {
                Expired = true; 

                AddDomainEvent(new BookRentExpired(this, book));
            }
            AddDomainEvent(new BookRentPassed(this, book));
        }
    }
}

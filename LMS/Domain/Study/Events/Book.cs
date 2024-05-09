using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class BookCreated(BookEntity book) : DomainEvent 
    {
        public BookEntity Book { get; set; } = book; 
    }

    public class BookRentCreated(BookRentEntity book) : DomainEvent
    {
        public BookRentEntity BookRent { get; set; } = book;
    }

    public class BookRentNotified(BookRentEntity book) : DomainEvent {
        public BookRentEntity BookRent { get; set; } = book;
    }

    public class BookRentExpired(BookRentEntity bookRent, BookEntity book) : DomainEvent {
        public BookRentEntity BookRent { get; set; } = bookRent;
        public BookEntity Book { get; set; } = book; 
    }

    public class BookRentPassed(BookRentEntity bookRent, BookEntity book) : DomainEvent {
        public BookRentEntity BookRent { get; set; } = bookRent;
        public BookEntity Book { get; set; } = book;
    }
}

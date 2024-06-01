using Hangfire;
using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Application.User;
using LMS.Application.User.Dto;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Books
{
    public class RentBook : BaseUseCase<RentBookDto, BookRentEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IUserService _userService { get; }

        public RentBook(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy, 
            IUserService userService)
        {
            _userService = userService; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<BookRentEntity> Execute(RentBookDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.extend, typeof(BookEntity), member, dto.BookId);
            var student = await _context.Students.FirstOrDefaultAsync(x =>
                x.Id == dto.StudentId && x.InstitutionMember.InstitutionId == dto.InstitutionId); 

            var book = await _context.Books.FirstOrDefaultAsync(
                x => x.Id == dto.BookId && x.InstitutionId == dto.InstitutionId);

            Guard.Against.NotFound(dto.BookId, book); 
            Guard.Against.NotFound(dto.StudentId, student);

            var rentBook = BookRentEntity.Create(book, student.Id, DateTime.UtcNow, dto.EndTime);

            BackgroundJob.Schedule(() => 
                _userService.CreateNotification().Execute(new CreateNotificationDto()
                    {
                        UserId = student.User.Id, 
                        Title = $"Сдайте книгу {book.Name} в учебное заведение", 
                        Text = "X"
                    }).Wait(), 
                rentBook.EndDate - new DateTime(DateTime.Now.Year, DateTime.Now.Month, day: 1)
            );

            _context.BookRents.Add(rentBook);
            await _context.SaveChangesAsync(); 

            return rentBook; 
        }
    }
}

using LMS.Application.Common.Interfaces;
using LMS.Application.User;
using LMS.Application.User.Dto;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Events;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.EventHandlers
{
    public class BookExpiredHandler : IEventSubscriber<BookRentExpired>
    {
        private IUserService _userService;

        public BookExpiredHandler(IUserService userService)
        {
            _userService = userService; 
        }

        public async Task HandleEvent(BookRentExpired @event, IApplicationDbContext dbContext)
        {
            var student = await dbContext.Students
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == @event.BookRent.StudentId);

            Guard.Against.NotFound(@event.BookRent.StudentId, student); 

            await _userService.CreateNotification().Execute(
                new CreateNotificationDto() { 
                    UserId = student.User.Id, 
                    Title = "Вы не сдали книгу во время", 
                    Text = $"Книга {@event.Book.Name} не была сдана вовремя, " +
                    $"вы должны были её сдать в ${@event.BookRent.EndDate}"
                }); 
        }
    }
}

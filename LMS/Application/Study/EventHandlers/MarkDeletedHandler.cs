using LMS.Application.Common.Interfaces;
using LMS.Application.User.Dto;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Events;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.EventHandlers
{
    public class MarkDeletedHandler : IEventSubscriber<InstitutionMarkedDeleted>
    {
        private IUserService _userService;

        public MarkDeletedHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleEvent(InstitutionMarkedDeleted @event, IApplicationDbContext dbContext)
        {
            var users = await dbContext.Users
                .Join(dbContext.InstitutionMembers,
                    user => user.Id,
                    member => member.UserId,
                    (user, member) => new
                    {
                        UserId = user.Id,
                        InstitutionId = member.InstitutionId
                    })
                .Where(x => x.InstitutionId == @event.Institution.Id)
                .ToListAsync();

            foreach (var user in users)
            {
                await _userService.CreateNotification().Execute(new User.Dto.CreateNotificationDto()
                {
                    Title = $"Заведение {@event.Institution.Name} будет удалено по причине: {@event.Reason}",
                    Text = "123",
                    UserId = user.UserId,
                });
            }
        }
    }
}

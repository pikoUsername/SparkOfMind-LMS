using LMS.Application.Common.Interfaces;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Events;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LMS.Application.Study.EventHandlers
{
    public class InstitutionBlockedHandler : IEventSubscriber<InstitutionBlocked>
    {
        private ILogger _logger;
        private IUserService _userService; 

        public InstitutionBlockedHandler(ILogger<InstitutionBlockedHandler> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleEvent(InstitutionBlocked @event, IApplicationDbContext dbContext)
        {
            _logger.LogInformation($"User: {@event.BlockedBy.Id} Blocked institution: {@event.Institution.Id}");

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
                    Title = $"Заведение {@event.Institution.Name} заблокировано по причине: {@event.Reason}",
                    Text = "123",
                    UserId = user.UserId,
                });
            }
        }
    }
}

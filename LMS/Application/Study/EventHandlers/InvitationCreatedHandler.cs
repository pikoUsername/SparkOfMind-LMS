using LMS.Application.Common.Interfaces;
using LMS.Application.User.Dto;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Events;
using LMS.Domain.Study.Services;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.EventHandlers
{
    public class InvitationCreatedHandler : IEventSubscriber<InvitationCreated>
    {
        private IUserService _userService;

        public InvitationCreatedHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleEvent(InvitationCreated @event, IApplicationDbContext dbContext)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == @event.UserId);

            Guard.Against.NotFound(@event.UserId, user);

            var institution = await dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == @event.Invitation.InstiutionId);
            Guard.Against.NotFound(@event.Invitation.InstiutionId, institution);

            await _userService.CreateNotification().Execute(
                new CreateNotificationDto()
                {
                    UserId = user.Id,
                    Title = "У вас есть приглашение",
                    Text = $"Вас пригласили в заведение принять приглашение: {InvitationService.CreateLink(@event.Invitation.Id)}. " + 
                    $"Имя заведения: {institution.Name}"
                });
        }
    }
}

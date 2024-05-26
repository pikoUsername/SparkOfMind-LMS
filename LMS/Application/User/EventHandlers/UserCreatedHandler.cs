using LMS.Application.Common.Interfaces;
using LMS.Domain.Payment.Entities;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Events;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Application.User.EventHandlers
{
    public class UserCreatedHandler : IEventSubscriber<UserCreated>
    {
        private ILogger _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEvent(UserCreated eventValue, IApplicationDbContext _context)
        {
            var wallet = WalletEntity.Create(eventValue.User);

            _context.Wallets.Add(wallet);

            _logger.LogInformation($"User created with id: {eventValue.User.Id}");

            eventValue.User.Permissions.AddPermissionWithCode(wallet, PermissionEnum.all); 

            return Task.CompletedTask;
        }
    }
}

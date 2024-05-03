using LMS.Application.Common.Interfaces;
using LMS.Domain.Messaging.Entities;
using LMS.Domain.Payment.Entities;
using LMS.Domain.User.Events;
using LMS.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

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
            var supportChat = ChatEntity.CreateSupport(eventValue.User);

            var wallet = WalletEntity.Create(eventValue.User);

            _context.Wallets.Add(wallet);

            _context.Chats.Add(supportChat);

            _logger.LogInformation($"User created with id: {eventValue.User.Id}");

            return Task.CompletedTask;
        }
    }
}

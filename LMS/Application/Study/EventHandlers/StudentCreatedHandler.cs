using LMS.Application.Common.Interfaces;
using LMS.Domain.Study.Events;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Application.Study.EventHandlers
{
    public class StudentCreatedHandler : IEventSubscriber<StudentCreated>
    {
        private ILogger _logger; 

        public StudentCreatedHandler(ILogger<StudentCreatedHandler> logger) {
            _logger = logger; 
        }

        public async Task HandleEvent(StudentCreated @event, IApplicationDbContext dbContext)
        {
            _logger.LogInformation($"Student with id: {@event.Student.Id}"); 
        }
    }
}

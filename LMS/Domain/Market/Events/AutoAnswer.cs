using LMS.Domain.Market.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Market.Events
{
    public class AutoAnswerIsUsed(AutoAnswerEntity answer) : BaseEvent
    {
        public AutoAnswerEntity AutoAnswersEntity { get; set; } = answer;
    }

    public class AutoAnswerCreated(AutoAnswerEntity answer) : BaseEvent
    {
        public AutoAnswerEntity AutoAnswersEntity { get; set; } = answer;
    }
}

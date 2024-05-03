using LMS.Domain.Market.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Market.Events
{
    public class GameCreated(GameEntity game) : BaseEvent
    {
        public GameEntity Game { get; set; } = game;
    }

    public class GameDeleted(GameEntity game, DateTime deletedAt) : BaseEvent
    {
        public GameEntity Game { get; set; } = game;
        public DateTime DeletedAt { get; set; } = deletedAt;
    }
}

using LMS.Domain.Messaging.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class ChatQueries
    {
        public static IQueryable<ChatEntity> IncludeStandard(this IQueryable<ChatEntity> query)
        {
            return query
                .Include(x => x.Participants)
                .Include(x => x.Messages)
                //.Include(x => x.Owner)
                .Include(x => x.Image)
                .Include(x => x.ReadRecords);
        }
    }
}

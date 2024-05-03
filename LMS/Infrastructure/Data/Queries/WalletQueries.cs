using LMS.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class WalletQueries
    {
        public static IQueryable<WalletEntity> IncludeStandard(this IQueryable<WalletEntity> query)
        {
            return query.
                Include(x => x.User);
        }
    }
}

using LMS.Domain.Study.Entities;

namespace LMS.Infrastructure.Data.Queries
{
    public static class BookQueries
    {
        public static IQueryable<BookEntity> IncludeStandard(this IQueryable<BookEntity> query)
        {
            return query; 
        }
    }
}

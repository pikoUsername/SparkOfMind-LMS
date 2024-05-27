using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class CourseGroupQueries
    {
        public static IQueryable<CourseGroupEntity> IncludeStandard(this IQueryable<CourseGroupEntity> query)
        {
            return query
                .Include(x => x.Students)
                .Include(x => x.GradeType);
        }
    }
}

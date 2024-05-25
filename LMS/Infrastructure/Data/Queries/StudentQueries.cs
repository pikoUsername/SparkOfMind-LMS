using LMS.Domain.Payment.Entities;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class StudentQueries
    {
        public static IQueryable<StudentEntity> IncludeStandard(this IQueryable<StudentEntity> query)
        {
            return query
                .Include(x => x.User)
                .Include(x => x.Courses)
                .Include(x => x.InstitutionMember); 
        }
    }
}

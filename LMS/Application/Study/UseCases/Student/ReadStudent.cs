using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Student
{
    public class GetStudent : BaseUseCase<GetStudentDto, StudentEntity?>
    {
        private IApplicationDbContext _context; 

        public GetStudent(IApplicationDbContext dbContext) { 
            _context = dbContext;
        }

        public async Task<StudentEntity?> Execute(GetStudentDto dto)
        {
            var query = _context.Students
                .Include(x => x.Courses)
                .Include(x => x.InstitutionMember)
                .Include(x => x.User)
                .AsQueryable(); 

            if (dto.Fullname != null) 
                query = query.Where(x => x.User.Fullname == dto.Fullname);
            if (dto.StudentId != null)
                query = query.Where(x => x.Id == dto.StudentId);
            if (dto.Phone != null)
                query = query.Where(x => x.Phone == dto.Phone);     

            var result = await query.FirstOrDefaultAsync();

            return result;
        }
    }
}

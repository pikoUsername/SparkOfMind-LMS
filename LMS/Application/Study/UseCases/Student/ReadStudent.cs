using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Application.User.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
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

    public class GetStudentsList : BaseUseCase<GetStudentListDto, ICollection<StudentEntity>>
    {
        private IApplicationDbContext _context { get; set; }
        private IAccessPolicy _accessPolicy { get; set; }
        private IInstitutionAccessPolicy _insitutionPolicy { get; set; }

        public GetStudentsList(
            IApplicationDbContext context,
            IAccessPolicy accessPolicy,
            IInstitutionAccessPolicy insitutionPolicy)
        {
            _context = context;
            _accessPolicy = accessPolicy;
            _insitutionPolicy = insitutionPolicy;
        }

        public async Task<ICollection<StudentEntity>> Execute(GetStudentListDto dto)
        {
            var query = _context.Students
                .Include(x => x.Courses)
                .Include(x => x.InstitutionMember)
                .Include(x => x.User)
                .AsQueryable();

            if (!await _accessPolicy.IsAllowed(Domain.User.Enums.PermissionEnum.read, typeof(StudentEntity)))
            {
                var member = await _insitutionPolicy.GetMemberByCurrentUser(dto.InstitutionId ?? Guid.NewGuid());
            }

            if (dto.InstitutionId != null)
                query = query.Where(x => x.InstitutionMember.InstitutionId == dto.InstitutionId);

            query = query.Paginate(dto.Start, dto.Ends); 

            return await query.ToListAsync();
        }
    }
}

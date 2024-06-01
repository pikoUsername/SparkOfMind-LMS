using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class GetGroup : BaseUseCase<GetGroupDto, CourseGroupEntity?>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;

        public GetGroup(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<CourseGroupEntity?> Execute(GetGroupDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);

            // anyone in institution can access other groups information EXCEPT students
            var group = await _context.CourseGroups
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.GroupId && x.Id == dto.InstitutionId);
            if (group == null) {
                return null; 
            }

            var student = await _context.Students.FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);
            if (student != null && group.Students.FirstOrDefault(x => x.Id == student.Id) == null) {
                throw new AccessDenied("Students are not allowed to see other groups"); 
            }

            return group;
        }
    }

    public class GetGroupsList : BaseUseCase<GetGroupsListDto, ICollection<CourseGroupEntity>>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;

        public GetGroupsList(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<ICollection<CourseGroupEntity>> Execute(GetGroupsListDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);

            // anyone in institution can access other groups information EXCEPT students
            var student = await _context.Students.FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);

            var query = _context.CourseGroups
                .Where(x => x.InstitutionId == dto.InstitutionId)
                .Paginate(dto.Start, dto.Ends)
                .IncludeStandard()
                .AsQueryable();
            if (student != null)
                query = query.Where(x => !x.Students.Any(x => x.Id == student.Id)); 

            var groups = await query.ToListAsync();

            return groups;
        }
    }
}

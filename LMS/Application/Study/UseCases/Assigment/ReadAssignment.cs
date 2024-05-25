using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class GetAssignment : BaseUseCase<GetAssignmentDto, AssignmentEntity?>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public GetAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<AssignmentEntity?> Execute(GetAssignmentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(Domain.User.Enums.PermissionEnum.read, typeof(AssignmentEntity), member, dto.AssignmentId);

            return await _context.Assigments
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.AssignmentId);
        }
    }

    public class GetAssignmentList : BaseUseCase<GetAssignmentListDto, ICollection<AssignmentEntity>>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public GetAssignmentList(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<ICollection<AssignmentEntity>> Execute(GetAssignmentListDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(Domain.User.Enums.PermissionEnum.read, typeof(AssignmentEntity), member);

            return await _context.Assigments
                .IncludeStandard()
                .Where(x => x.AssignedGroupId == dto.GroupCourseId)
                .Where(x => x.CourseId == dto.CourseId)
                .ToListAsync();
        }
    }
}

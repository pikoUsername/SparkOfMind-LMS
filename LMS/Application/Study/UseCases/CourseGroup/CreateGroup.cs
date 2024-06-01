using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class CreateGroup : BaseUseCase<CreateGroupDto, CourseGroupEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public CreateGroup(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<CourseGroupEntity> Execute(CreateGroupDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(PermissionEnum.write, typeof(CourseGroupEntity), member);

            var gradeType = await _context.GradeTypes.FirstOrDefaultAsync(x => x.Id == dto.GradeTypeId);

            Guard.Against.Null(gradeType, message: "Grade type does not exists"); 

            var group = CourseGroupEntity.Create(dto.Name, dto.InstitutionId, gradeType, dto.CourseId);

            var user = await _accessPolicy.GetCurrentUser();

            user.Permissions.AddPermissionWithCode(group, PermissionEnum.all); 
            _context.CourseGroups.Add(group);
            await _context.SaveChangesAsync(); 

            return group;
        }
    }
}

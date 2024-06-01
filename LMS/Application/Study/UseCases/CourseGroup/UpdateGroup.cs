using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class UpdateGroup : BaseUseCase<UpdateGroupDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }


        public UpdateGroup(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(UpdateGroupDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(CourseGroupEntity), member, dto.GroupId);

            var group = await _context.CourseGroups.FirstOrDefaultAsync(x => x.Id == dto.GroupId);
            Guard.Against.Null(group, message: "Group does not exists"); 
            
            if (!string.IsNullOrEmpty(dto.Name))
                group.Name = dto.Name;
            if (dto.GradeTypeId != null)
            {
                var gradeType = await _context.GradeTypes.FirstOrDefaultAsync(x => x.Id == dto.GradeTypeId);

                Guard.Against.Null(gradeType, message: "Grade type does not exists");

                group.GradeType = gradeType;
            }
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class ExtendGroup : BaseUseCase<ExtendGroupDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public ExtendGroup(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(ExtendGroupDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.extend, typeof(GroupEntity), member, dto.GroupId);

            var group = await _context.CourseGroups.FirstOrDefaultAsync(x =>
                x.Id == dto.GroupId && x.InstitutionId == dto.InstitutionId);

            Guard.Against.Null(group, message: "Group does not exists");

            var student = await _context.Students.FirstOrDefaultAsync(x => 
                x.Id == dto.StudentId && x.InstitutionMember.InstitutionId == dto.InstitutionId);

            Guard.Against.Null(student, message: "Student does not exists"); 

            group.AddStudents(student); 
            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}

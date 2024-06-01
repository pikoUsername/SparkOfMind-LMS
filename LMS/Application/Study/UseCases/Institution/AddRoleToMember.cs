using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class AddRoleToMember : BaseUseCase<AddRoleToMemberDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public AddRoleToMember(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(AddRoleToMemberDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 
            await _institutionPolicy.EnforcePermission(PermissionEnum.all, typeof(InstitutionRolesEntity), member);

            var role = await _context.InstitutionRoles.FirstOrDefaultAsync(
                x => x.Id == dto.RoleId && x.InstitutionId == dto.InstitutionId);

            Guard.Against.NotFound(dto.RoleId, role);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == member.UserId);

            Guard.Against.NotFound(member.UserId, user);

            await _context.Entry(member).Collection("Roles").LoadAsync();
            await _context.Entry(member.Roles).Collection("Permissions").LoadAsync();

            member.AddRoles(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

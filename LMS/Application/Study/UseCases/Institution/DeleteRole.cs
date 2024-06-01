using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteInstitutionRole : BaseUseCase<DeleteRoleDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public DeleteInstitutionRole(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteRoleDto dto)
        {
            if (!await _institutionPolicy.IsOwner(dto.InstitutionId))
            {
                throw new AccessDenied("You are not owner");
            };

            var role = await _context.InstitutionRoles.FirstOrDefaultAsync(x => x.Id == dto.RoleId);

            Guard.Against.NotFound(dto.RoleId, role);

            _context.InstitutionRoles.Remove(role);
            await _context.SaveChangesAsync(); 

            return true;
        }
    }

    public class DeleteRoleFromMember : BaseUseCase<DeleteRoleFromMemberDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public DeleteRoleFromMember(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteRoleFromMemberDto dto)
        {
            if (!await _institutionPolicy.IsOwner(dto.InstitutionId))
            {
                throw new AccessDenied("You are not owner"); 
            };
            var member = await _context.InstitutionMembers
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == dto.MemberId);

            Guard.Against.NotFound(dto.MemberId, member); 

            member.DeleteRole(dto.RoleId);

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}

using LMS.Application.Common.Interfaces;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure
{
    public class InstitutionAccessPolicy : IInstitutionAccessPolicy
    {
        private IApplicationDbContext _context;

        public InstitutionAccessPolicy(IApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task EnforceRole(InstitutionRolesEntity role, InstitutionMemberEntity member)
        {

        }
        public async Task EnforceRole(string roleName, InstitutionMemberEntity member)
        {

        }
        public async Task EnforcePermission(string permissionAcl, InstitutionMemberEntity member)
        {

        }
        public async Task EnforcePermission(PermissionEnum action, string subjectName, InstitutionMemberEntity member)
        {

        }
        public async Task<InstitutionMemberEntity> GetMember(Guid userId, Guid institutionId)
        {
            var member = await _context.InstitutionMembers
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.InstitutionId == institutionId);

            Guard.Against.Null(member);

            return member; 
        }
    }
}

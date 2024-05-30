using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class AcceptMembership : BaseUseCase<AcceptMembershipDto, InstitutionMemberEntity>
    {   
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public AcceptMembership(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<InstitutionMemberEntity> Execute(InviteMemberDto dto)
        {
            var user = await _accessPolicy.GetCurrentUser();
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);
            var oldMember = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            if (oldMember != null)
                throw new AccessDenied("Member already exists"); 

            Guard.Against.NotFound(dto.InstitutionId, institution); 

            if (institution.OwnerId != user.Id)
            {
                throw new AccessDenied("you are not owner"); 
            }

            var member = InstitutionMemberEntity.Create(user.Id, institution.Id);

            _context.InstitutionMembers.Add(member);
            if (dto.IsTeacher)
            {
                var teacher = TeacherEntity.CreateWithMember(
                    dto.InstitutionId, member, user, Domain.Study.Enums.TeacherStatus.Active);

                _context.Teachers.Add(teacher); 
            }

            await _context.SaveChangesAsync(); 

            return member;
        }
    }
}

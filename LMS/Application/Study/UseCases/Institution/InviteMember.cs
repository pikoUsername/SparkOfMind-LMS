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

        public async Task<InstitutionMemberEntity> Execute(AcceptMembershipDto dto)
        {
            var user = await _accessPolicy.GetCurrentUser();
            var invitation = await _context.Invitations.FirstOrDefaultAsync(x => x.Id == dto.InvitationId);

            Guard.Against.NotFound(dto.InvitationId, invitation); 

            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == invitation.InstiutionId);
            var oldMember = await _institutionPolicy.GetMemberByCurrentUser(invitation.InstiutionId);
            if (oldMember != null)
                throw new AccessDenied("Member already exists"); 

            Guard.Against.NotFound(invitation.InstiutionId, institution); 

            if (institution.OwnerId != user.Id)
            {
                throw new AccessDenied("you are not owner"); 
            }

            var member = InstitutionMemberEntity.Create(user.Id, institution.Id);

            _context.InstitutionMembers.Add(member);
            if (invitation.IsTeacher)
            {
                var teacher = TeacherEntity.CreateWithMember(
                    invitation.InstiutionId, member, user, Domain.Study.Enums.TeacherStatus.Active);

                _context.Teachers.Add(teacher); 
            }
            invitation.Complete(); 

            await _context.SaveChangesAsync(); 

            return member;
        }
    }

    public class InviteMember : BaseUseCase<InviteMemberDto, InvitationEntity> 
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }

        public InviteMember(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<InvitationEntity> Execute(InviteMemberDto dto)
        {
            var user = await _accessPolicy.GetCurrentUser();
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);

            Guard.Against.NotFound(dto.InstitutionId, institution);

            if (institution.OwnerId != user.Id)
            {
                throw new AccessDenied("you are not owner");
            }
            var invitation = InvitationEntity.Create(user.Id, institution.Id, dto.IsTeacher); 

            _context.Invitations.Add(invitation);

            await _context.SaveChangesAsync();

            return invitation;
        }
    }
}

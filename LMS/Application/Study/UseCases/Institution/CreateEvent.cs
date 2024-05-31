using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateEvent : BaseUseCase<CreateEventDto, InstitutionEventEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public CreateEvent(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<InstitutionEventEntity> Execute(CreateEventDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 
            await _institutionPolicy.EnforcePermission(Domain.User.Enums.PermissionEnum.write, typeof(InstitutionEventEntity), member);

            var byUser = await _context.Users
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == member.UserId);

            Guard.Against.NotFound(member.UserId, byUser); 

            var @event = InstitutionEventEntity.Create(dto.Title, dto.Text, dto.InstitutionId, byUser, dto.StartsAt, dto.EndsAt);

            byUser.Permissions.AddPermissionWithCode(@event, Domain.User.Enums.PermissionEnum.all); 

            _context.InstitutionEvents.Add(@event);
            await _context.SaveChangesAsync();

            return @event;
        }
    }
}

using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateEvent : BaseUseCase<UpdateEventDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public UpdateEvent(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(UpdateEventDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(InstitutionEventEntity), member, dto.EventId); 

            var insEvent = await _context.InstitutionEvents.FirstOrDefaultAsync(x => x.Id == dto.EventId);

            Guard.Against.NotFound(dto.EventId, insEvent); 

            if (!string.IsNullOrEmpty(dto.Title)) {
                insEvent.Title = dto.Title; 
            }
            if (!string.IsNullOrEmpty(dto.Text))
            {
                insEvent.Text = dto.Text;   
            }
            if (dto.EndsAt != null)
            {
                insEvent.EndDate = dto.EndsAt;
            }
            if (dto.StartsAt != null)
            {
                insEvent.StartDate = dto.StartsAt;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using Hangfire;
using Hangfire.Dashboard;
using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteInstitution : BaseUseCase<DeleteInstitutionDto, bool>
    {
        // cannot be deleted directly, it has two steps: 
        // First, owner starts institution deletion proccess, and marks for deletion. 
        // Then, all members of institution will be notified including owner
        // In Second step, 1-5 days task will hang around, only then all institution will be deleted. 
        // Around 1-5 days, that task can be canceled. 
        // 1-5 days, because it will depend on insitution size, if it's big, then deletion task 
        // will hang 5 days. if it's small like 1-2 members or 0, it will be deleted in one day at night 
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public DeleteInstitution(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteInstitutionDto dto)
        {
            await _accessPolicy.EnforceRole(Domain.User.Enums.UserRoles.Admin);

            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);

            Guard.Against.NotFound(dto.InstitutionId, institution); 
            var user = await _accessPolicy.GetCurrentUser(); 

            institution.MarkDelete(dto.Reason, user);

            var membersCount = await _context.InstitutionMembers
                .Where(x => x.InstitutionId == dto.InstitutionId)
                .CountAsync();
            DateTime timeDelete; 
            if (membersCount > 5)
            {
                timeDelete = DateTime.UtcNow.AddDays(1); 
            } else if (membersCount > 15) 
            {
                timeDelete = DateTime.UtcNow.AddDays(5);                 
            } else
            {

                timeDelete = DateTime.UtcNow.AddMinutes(1);
            }

            // TODO: Hangfire не знает как создовать контекст в методе, поэтому надо переместить вызов этой функции в класс для DI
            ScheduleInstitutionDeletion(institution, timeDelete); 

            await _context.SaveChangesAsync(); 

            return true;
        }

        public async Task DeleteInstitutionAsync(InstitutionEntity institution)
        {
            _context.Institutions.Remove(institution);
            await _context.InstitutionMembers
                .Where(member => member.InstitutionId == institution.Id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public void ScheduleInstitutionDeletion(InstitutionEntity institution, DateTime delay)
        {
            BackgroundJob.Schedule(() => DeleteInstitutionSyncWrapper(institution), delay);
        }

        private void DeleteInstitutionSyncWrapper(InstitutionEntity institution)
        {
            DeleteInstitutionAsync(institution).GetAwaiter().GetResult();
        }
    }
}

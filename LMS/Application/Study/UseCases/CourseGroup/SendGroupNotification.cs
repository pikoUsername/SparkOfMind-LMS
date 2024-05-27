using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Application.User.Interfaces;
using LMS.Domain.Files.Entities;
using LMS.Domain.User.Entities;
using LMS.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class SendGroupNotification : BaseUseCase<SendGroupNotificationDto, bool>
    {
        private IApplicationDbContext _context; 
        private IInstitutionAccessPolicy _institutionPolicy; 
        private INotificationCache _notificationCache; 

        public SendGroupNotification(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            INotificationCache notificationCache)
        {
            _notificationCache = notificationCache; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(SendGroupNotificationDto dto)
        {
            // anyone can send notifications into groups except students, because i m lazy to make roles or permissions 
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            
            var group = await _context.CourseGroups
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == dto.GroupId);
            Guard.Against.Null(group, message: "Group does not exists");

            ICollection<FileEntity> Files = []; 

            foreach (var student in group.Students) {
                var notification = NotificationEntity.CreateFromUser(
                    dto.Title, dto.Text, student.User.Id, member.UserId, []);

                _context.Notifications.Add(notification);

                await _notificationCache.AddNotification(student.User.Id, notification);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.User.Dto;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.User.UseCases
{
    public class GetNotifications : BaseUseCase<GetNotificationsDto, ICollection<NotificationEntity>>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;

        public GetNotifications(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<ICollection<NotificationEntity>> Execute(GetNotificationsDto dto)
        {
            var query = _context.Notifications.AsQueryable();

            if (dto.UserId != null)
            {
                await _accessPolicy.FailIfNotSelfOrNoAccess(
                    (Guid)dto.UserId, UserRoles.Moderator);

                query = query.Where(x => x.ToUserId == dto.UserId);
            }
            if (dto.Role != null)
            {
                await _accessPolicy.FailIfNoAccess(UserRoles.Moderator);

                query = query
                    .Join(
                        _context.Users,
                        notification => notification.ToUserId,
                        user => user.Id,
                        (notification, user) => new { Notification = notification, User = user }
                    )
                    .Where(x => x.User.Role == dto.Role)
                    .Select(x => x.Notification);
            }

            //query = query.Paginate(dto.Start, dto.Ends); 

            return await query.AsNoTracking().ToListAsync();
        }
    }
}

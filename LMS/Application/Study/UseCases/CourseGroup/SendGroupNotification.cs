using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class SendGroupNotification : BaseUseCase<SendGroupNotificationDto, bool>
    {
        public SendGroupNotification() { }

        public async Task<bool> Execute(SendGroupNotificationDto dto)
        {
            return true;
        }
    }
}

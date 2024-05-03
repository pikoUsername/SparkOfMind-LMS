using Microsoft.OpenApi.Any;
using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.User.Events
{
    public class UserUpdated(
        UserEntity user,
        string fieldName
    ) : BaseEvent
    {
        public string FieldName { get; set; } = fieldName;
        public UserEntity User { get; } = user;
    }
}

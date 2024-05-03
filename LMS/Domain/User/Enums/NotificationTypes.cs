using System.Text.Json.Serialization;

namespace LMS.Domain.User.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotificationTypes
    {
        Other,
        Purchase,
        Buy,
        Wallet,
        Withdraw,
        System,
    }
}

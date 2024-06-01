using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.Payment.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PurchaseStatus
    {
        Teaching,   
        Processing,
        Failed,
        Success,
        Rejected,
        Expired,
    }
}

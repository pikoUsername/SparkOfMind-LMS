using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.Payment.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyEnum
    {
        [Display(Name = "RUB")]
        RussianRuble,
        [Display(Name = "USD")]
        Dollar,
        [Display(Name = "EUR")]
        Euro
    }
}

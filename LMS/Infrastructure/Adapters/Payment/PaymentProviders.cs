using System.Text.Json.Serialization;

namespace LMS.Infrastructure.Adapters.Payment
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentProviders
    {
        BankCardRu,
        Balance,
        Test
    }
}

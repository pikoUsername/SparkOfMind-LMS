using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.Market.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameTypes
    {
        [Display(Name = "GAME")]
        Game,
        [Display(Name = "APPLICATION")]
        Application
    }
}

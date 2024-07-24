using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UTEHY.DatabaseCoursePortal.Api.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VerificationType
    {
        [Description("Phone")]
        Phone = 1,

        [Description("Email")]
        Email = 2,
    }
}

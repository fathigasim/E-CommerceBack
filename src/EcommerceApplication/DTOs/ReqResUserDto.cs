using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceApplication.DTOs
{
    public class ReqResUserDto
    {
        [JsonPropertyName("data")]
        public ReqResData ReqResData { get; set; } = default!;
    }
    public record ReqResData(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("first_name")] string FirstName,
        [property: JsonPropertyName("last_name")] string LastName,
        [property: JsonPropertyName("avatar")] string Avatar
    );

}

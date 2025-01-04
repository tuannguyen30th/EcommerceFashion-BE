using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.AccountService.Models.RequestModels
{
    public class GoogleUserInformationModel
    {
        [JsonPropertyName("given_name")] public string FirstName { get; set; } = null!;
        [JsonPropertyName("family_name")] public string LastName { get; set; } = null!;
        [JsonPropertyName("email")] public string Email { get; set; } = null!;
        [JsonPropertyName("picture")] public string Image { get; set; } = null!;
    }
}

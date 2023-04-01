using Newtonsoft.Json;
using System;
using System.Linq;

namespace zero.Shared.Models.Security;

public class TokenResponse : LogableModel
{
    public string AccessToken { get; set; }

    public string TokenType { get; set; }

    public double ExpiresIn { get; set; }

    public string RefreshToken { get; set; }

    public DateTimeOffset? RefreshTokenExpires { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public string SystemId { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string LanguageId { get; set; }

    [JsonProperty("roles")]
    public string Roles { get; set; }

    public DateTimeOffset? Issued { get; set; }

    public DateTimeOffset? Expires { get; set; }

    public bool IsInRole(string role)
    {
        if (string.IsNullOrEmpty(Roles)) { return false; }

        return Roles.Split(',').Contains(role);
    }
}
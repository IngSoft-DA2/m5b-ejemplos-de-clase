using Cine.BusinessLogic.Abstractions;

namespace Cine.BusinessLogic;

public sealed class SessionService : ISessionService
{
    private const string ValidUsername = "admin";
    private const string ValidPassword = "admin123";
    private const string ValidToken = "SecretPassword";

    public string? Authenticate(string username, string password)
    {
        if (!string.Equals(username, ValidUsername, StringComparison.OrdinalIgnoreCase) || password != ValidPassword)
        {
            return null;
        }

        return ValidToken;
    }

    public bool IsTokenValid(string token)
    {
        return string.Equals(token, ValidToken, StringComparison.Ordinal);
    }
}

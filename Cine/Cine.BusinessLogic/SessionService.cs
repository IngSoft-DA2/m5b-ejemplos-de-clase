using Cine.BusinessLogic.Abstractions;

namespace Cine.BusinessLogic;

public sealed class SessionService : ISessionService
{
    private static readonly HashSet<string> ValidTokens =
    [
        "Asimetria"
    ];

    public bool IsTokenValid(string token)
    {
        return ValidTokens.Contains(token);
    }
}

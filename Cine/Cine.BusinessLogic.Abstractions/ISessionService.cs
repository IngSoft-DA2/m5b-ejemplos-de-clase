namespace Cine.BusinessLogic.Abstractions;

public interface ISessionService
{
    string? Authenticate(string username, string password);
    bool IsTokenValid(string token);
}

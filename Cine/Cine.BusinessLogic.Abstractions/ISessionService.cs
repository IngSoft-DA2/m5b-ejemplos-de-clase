namespace Cine.BusinessLogic.Abstractions;

public interface ISessionService
{
    bool IsTokenValid(string token);
}

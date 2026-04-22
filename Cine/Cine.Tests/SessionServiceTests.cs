using Cine.BusinessLogic;

namespace Cine.Tests;

[TestClass]
public sealed class SessionServiceTests
{
    [TestMethod]
    public void Authenticate_WhenCredentialsAreValid_ReturnsTokenThatBecomesValid()
    {
        var service = new SessionService();

        var token = service.Authenticate("admin", "admin123");

        Assert.AreEqual("Asimetria", token);
        Assert.IsTrue(service.IsTokenValid(token!));
    }

    [TestMethod]
    public void Authenticate_WhenCredentialsAreInvalid_ReturnsNull()
    {
        var service = new SessionService();

        var token = service.Authenticate("admin", "incorrecta");

        Assert.IsNull(token);
    }
}

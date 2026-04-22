using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Cine.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cine.Tests;

[TestClass]
public sealed class AuthControllerTests
{
    private Mock<ISessionService> _sessionServiceMock = null!;
    private AuthController _controller = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
        _controller = new AuthController(_sessionServiceMock.Object);
    }

    [TestMethod]
    public void Login_WhenCredentialsAreValid_ReturnsToken()
    {
        var request = new LoginRequestDto { Username = "admin", Password = "admin123" };
        _sessionServiceMock.Setup(s => s.Authenticate("admin", "admin123")).Returns("token-123");

        var response = _controller.Login(request);

        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        var dto = ok.Value as LoginResponseDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("token-123", dto.Token);
        _sessionServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Login_WhenCredentialsAreInvalid_ReturnsUnauthorized()
    {
        var request = new LoginRequestDto { Username = "admin", Password = "bad-password" };
        _sessionServiceMock.Setup(s => s.Authenticate("admin", "bad-password")).Returns((string?)null);

        var response = _controller.Login(request);

        Assert.IsInstanceOfType<UnauthorizedObjectResult>(response.Result);
        _sessionServiceMock.VerifyAll();
    }
}

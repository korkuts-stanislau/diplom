using Xunit;
using Auth.Services;
using Moq;
using Auth.Data.Interfaces;
using Microsoft.Extensions.Options;
using Common;
using System.Threading.Tasks;
using Auth.UIModels;
using Auth.Models;

namespace Tests.Auth;

public class AuthServiceTests
{
    private readonly AuthService _as;
    private readonly Mock<IOptions<AuthOptions>> _authOptionsMock = new Mock<IOptions<AuthOptions>>();
    private readonly Mock<IAccountRepository> _accRepoMock = new Mock<IAccountRepository>();
    private readonly PasswordHasherService _phs = new PasswordHasherService();
    

    public AuthServiceTests()
    {
        _as = new AuthService(_authOptionsMock.Object, _accRepoMock.Object, _phs);
    }

    [Fact]
    public async Task SignUp_UserShouldSignUp()
    {
        //Arrange
        AuthData data = new AuthData {
            Email = "newuser@gmail.com",
            Password = "strongpass123"
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(data.Email))
            .ReturnsAsync(default(Account));

        //Act
        var account = await _as.SignUp(data);
        //Assert
        Assert.Equal(data.Email, account.Email);
        Assert.Equal(Role.User, account.Roles[0]);
        Assert.Equal(1, account.Roles.Length);
    }
}
using Xunit;
using Auth.Services;
using Moq;
using Auth.Data.Interfaces;
using Microsoft.Extensions.Options;
using Common;
using System.Threading.Tasks;
using Auth.UIModels;
using Auth.Models;
using System;

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
    public async Task SignUp_UserCanSignUp()
    {
        //Arrange
        var email = "newuser@gmail.com";
        var password = "strongpass123";

        AuthData data = new AuthData {
            Email = email,
            Password = password
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync((Account)null!);

        //Act
        var account = await _as.SignUp(data);
        
        //Assert
        Assert.Equal(data.Email, account.Email);
        Assert.Equal(Role.User, account.Roles[0]);
        Assert.Equal(1, account.Roles.Length);
    }

    [Fact]
    public async Task SignUp_UserCanNotSignUp_UserWithSameEmailExists()
    {
        //Arrange
        var email = "newuser@gmail.com";
        var password = "strongpass123";

        AuthData data = new AuthData {
            Email = email,
            Password = password
        };
        Account userAccount = new Account {
            Email = email,
            PasswordHash = _phs.Hash(password),
            Roles = new Role[] {Role.User}
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        Func<Task> signUp = async () => await _as.SignUp(data);

        //Assert
        await Assert.ThrowsAsync<Exception>(signUp);
    }

    [Fact]
    public async Task SignUp_UserCanSignIn()
    {
        //Arrange
        var email = "newuser@gmail.com";
        var password = "strongpass123";

        AuthData data = new AuthData {
            Email = email,
            Password = password
        };
        Account userAccount = new Account {
            Email = email,
            PasswordHash = _phs.Hash(password),
            Roles = new Role[] {Role.User}
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        var account = await _as.SignIn(data);
        
        //Assert
        Assert.Equal(account.Email, userAccount.Email);
        Assert.Equal(account.Roles[0], userAccount.Roles[0]);
        Assert.Equal(account.PasswordHash, userAccount.PasswordHash);
    }

    [Fact]
    public async Task SignUp_UserCanNotSignIn_WrongPassword()
    {
        //Arrange
        var email = "newuser@gmail.com";
        var password = "strongpass123";
        var wrongPassword = "wrongPass";

        AuthData data = new AuthData {
            Email = email,
            Password = wrongPassword
        };
        Account userAccount = new Account {
            Email = email,
            PasswordHash = _phs.Hash(password),
            Roles = new Role[] {Role.User}
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        Func<Task> signIn = async() => await _as.SignIn(data);
        
        //Assert
        await Assert.ThrowsAsync<Exception>(signIn);
    }

    [Fact]
    public async Task SignUp_UserCanNotSignIn_WrongEmail()
    {
        //Arrange
        var wrongEmail = "wrong.email@wr.com";
        var password = "strongpass123";

        AuthData data = new AuthData {
            Email = wrongEmail,
            Password = password
        };

        _accRepoMock.Setup(x => x.GetByEmailAsync(wrongEmail))
            .ReturnsAsync((Account)null!);

        //Act
        Func<Task> signIn = async() => await _as.SignIn(data);
        
        //Assert
        await Assert.ThrowsAsync<Exception>(signIn);
    }
}
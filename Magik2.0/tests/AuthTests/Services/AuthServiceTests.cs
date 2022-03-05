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

namespace Tests.AuthTests.Services;

public class AuthServiceTests
{
    private readonly AuthService authService;
    private readonly Mock<IOptions<AuthOptions>> authOptionsMock = new Mock<IOptions<AuthOptions>>();
    private readonly Mock<IAccountRepository> accRepoMock = new Mock<IAccountRepository>();
    private readonly PasswordHasherService passHasherService = new PasswordHasherService();
    

    public AuthServiceTests()
    {
        authService = new AuthService(authOptionsMock.Object, accRepoMock.Object, passHasherService);
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

        accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync((Account)null!);

        //Act
        var account = await authService.SignUpAsync(data);
        
        //Assert
        Assert.Equal(data.Email, account.Email);
        Assert.Equal(Role.User, account.Roles[0]);
        Assert.Single(account.Roles);
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
            PasswordHash = passHasherService.Hash(password),
            Roles = new Role[] {Role.User}
        };

        accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        Func<Task> signUp = async () => await authService.SignUpAsync(data);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(signUp);
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
            PasswordHash = passHasherService.Hash(password),
            Roles = new Role[] {Role.User}
        };

        accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        var account = await authService.SignInAsync(data);
        
        //Assert
        Assert.Equal(userAccount.Email, account.Email);
        Assert.Equal(userAccount.Roles[0], account.Roles[0]);
        Assert.Equal(userAccount.PasswordHash, account.PasswordHash);
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
            PasswordHash = passHasherService.Hash(password),
            Roles = new Role[] {Role.User}
        };

        accRepoMock.Setup(x => x.GetByEmailAsync(email))
            .ReturnsAsync(userAccount);

        //Act
        Func<Task> signIn = async() => await authService.SignInAsync(data);
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(signIn);
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

        accRepoMock.Setup(x => x.GetByEmailAsync(wrongEmail))
            .ReturnsAsync((Account)null!);

        //Act
        Func<Task> signIn = async() => await authService.SignInAsync(data);
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>(signIn);
    }
}
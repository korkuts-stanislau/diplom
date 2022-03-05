using Xunit;
using Auth.Services;
using System.Threading.Tasks;

namespace Tests.AuthTests.Services;

public class PasswordHasherServiceTests
{
    private readonly PasswordHasherService passHasherService;

    public PasswordHasherServiceTests()
    {
        passHasherService = new PasswordHasherService();
    }

    [Fact]
    public void Hash_Verify()
    {
        //Arrange
        var password = "VeryStrongPassword123";
        var wrongPassword = "WrongPassword";

        //Act
        var hash = passHasherService.Hash(password);

        //Assert
        Assert.True(passHasherService.Verify(password, hash));
        Assert.False(passHasherService.Verify(wrongPassword, hash));
    }
}
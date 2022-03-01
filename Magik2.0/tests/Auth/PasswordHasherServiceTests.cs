using Xunit;
using Auth.Services;
using System.Threading.Tasks;

namespace Tests.Auth;

public class PasswordHasherServiceTests
{
    private readonly PasswordHasherService _phs;

    public PasswordHasherServiceTests()
    {
        _phs = new PasswordHasherService();
    }

    [Fact]
    public async Task Hash_Verify()
    {
        //Arrange
        var password = "VeryStrongPassword123";
        var wrongPassword = "WrongPassword";

        //Act
        var hash = _phs.Hash(password);

        //Assert
        Assert.True(_phs.Verify(password, hash));
        Assert.False(_phs.Verify(wrongPassword, hash));
    }
}
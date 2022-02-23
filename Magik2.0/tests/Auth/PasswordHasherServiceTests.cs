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
    public async Task Hash_Verify_SamePassword()
    {
        //Arrange

        //Act

        //Assert

    }
}
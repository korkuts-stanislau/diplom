using System;
using System.Threading.Tasks;
using Moq;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Services;
using Resource.Tools;
using Xunit;

namespace Tests.ResourceTests.Services;

public class ProfileServiceTests {
    private readonly ProfileService profileService;
    private readonly Mock<IProfileRepository> profileRepoMock = new Mock<IProfileRepository>();
    private readonly PictureConverter pictureConverter = new PictureConverter();

    public ProfileServiceTests()
    {
        profileService = new ProfileService(profileRepoMock.Object, pictureConverter);
    }

    [Fact]
    public async Task GetProfileOrDefaultAsync_NewUser() {
        //Arrange
        string accountId = Guid.NewGuid().ToString();

        profileRepoMock.Setup(x => x.FirstOrDefaultAsync(accountId))
            .ReturnsAsync((Profile)null!);

        //Act
        var profile = await profileService.GetProfileOrDefaultAsync(accountId);

        //Assert
        Assert.Null(profile);
    }

    [Fact]
    public async Task GetProfileOrDefaultAsync_CreateProfile() {
        //Arrange
        string accountId = Guid.NewGuid().ToString();
        string email = "test@gmail.com";

        //Act
        var createdProfile = await profileService.CreateProfileAsync(accountId, email);

        //Assert
        Assert.NotNull(createdProfile);
        Assert.Equal(email, createdProfile.Username);
        Assert.Equal("", createdProfile.Picture);
    }

    [Fact]
    public async Task GetProfileOrDefaultAsync_GetExistingProfile() {
        //Arrange
        string accountId = Guid.NewGuid().ToString();
        string email = "test@gmail.com";

        profileRepoMock.Setup(x => x.FirstOrDefaultAsync(accountId))
            .ReturnsAsync(new Profile {
                Id = 0,
                AccountId = accountId,
                Username = email,
                Picture = null,
                Description = "",
                Icon = null
            });

        //Act
        var profile = await profileService.GetProfileOrDefaultAsync(accountId);

        //Assert
        Assert.NotNull(profile);
        Assert.Equal(email, profile!.Username);
        Assert.Equal("", profile.Picture);
        Assert.Equal("", profile.Description);
    }
}
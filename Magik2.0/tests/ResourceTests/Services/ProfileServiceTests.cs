using System;
using System.Threading.Tasks;
using Moq;
using Resource.Data.Interfaces;
using Resource.MapperProfiles;
using Resource.Models;
using Resource.Services;
using Resource.Tools;
using Xunit;

namespace Tests.ResourceTests.Services;

public class ProfileServiceTests {
    private readonly ProfileService profileService;
    private readonly Mock<IUnitOfWork> uofMock = new Mock<IUnitOfWork>();
    private readonly Mock<IProfileRepository> profileRepoMock = new Mock<IProfileRepository>();
    private readonly PictureConverter pictureConverter = new PictureConverter();

    public ProfileServiceTests()
    {
        var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
        {
            mc.AddProfiles(new AutoMapper.Profile[] {
                new ProfileMapperProfile()
            });
        });
        AutoMapper.IMapper mapper = mappingConfig.CreateMapper();

        uofMock.Setup(x => x.Profiles)
            .Returns(profileRepoMock.Object);

        profileService = new ProfileService(uofMock.Object, pictureConverter, mapper);
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
        string username = email.Split("@")[0];

        //Act
        var createdProfile = await profileService.CreateProfileAsync(accountId, email);

        //Assert
        Assert.NotNull(createdProfile);
        Assert.Equal(username, createdProfile.Username);
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
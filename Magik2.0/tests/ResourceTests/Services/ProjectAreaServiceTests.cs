using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Services;
using Resource.Tools;
using Xunit;

namespace Tests.ResourceTests.Services;

public class ProjectAreaServiceTests {
    private readonly ProjectAreaService projectAreaService;
    private readonly Mock<IProjectAreaRepository> projectAreaRepoMock = new Mock<IProjectAreaRepository>();
    private readonly PictureConverter pictureConverter = new PictureConverter();
    private readonly UserAccessValidator accessValidator = new UserAccessValidator();
    
    public ProjectAreaServiceTests()
    {
        projectAreaService = new ProjectAreaService(projectAreaRepoMock.Object, pictureConverter, accessValidator);
    }

    [Fact]
    public async Task GetAsync_EmptyList() {
        //Arrange
        string accountId = Guid.NewGuid().ToString();

        projectAreaRepoMock.Setup(x => x.GetAsync(accountId))
            .ReturnsAsync(new List<ProjectArea>());

        //Act
        var projectAreas = await projectAreaService.GetProjectAreasAsync(accountId);

        //Assert
        Assert.Empty(projectAreas);
    }
}
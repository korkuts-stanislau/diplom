using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Resource.Data.Interfaces;
using Resource.MapperProfiles;
using Resource.Models;
using Resource.Services;
using Resource.Tools;
using Xunit;

namespace Tests.ResourceTests.Services;

public class ProjectAreaServiceTests {
    private readonly ProjectAreaService projectAreaService;
    private readonly Mock<IUnitOfWork> uofMock = new Mock<IUnitOfWork>();
    private readonly Mock<IProjectAreaRepository> projectAreaRepoMock = new Mock<IProjectAreaRepository>();
    private readonly PictureConverter pictureConverter = new PictureConverter();
    private readonly UserAccessValidator accessValidator;
    
    public ProjectAreaServiceTests()
    {
        var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
        {
            mc.AddProfiles(new AutoMapper.Profile[] {
                new ProjectAreaMapperProfile()
            });
        });
        AutoMapper.IMapper mapper = mappingConfig.CreateMapper();

        
        uofMock.Setup(x => x.ProjectAreas)
            .Returns(projectAreaRepoMock.Object);

        accessValidator = new UserAccessValidator(uofMock.Object);
        
        projectAreaService = new ProjectAreaService(uofMock.Object, pictureConverter, accessValidator, mapper);
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
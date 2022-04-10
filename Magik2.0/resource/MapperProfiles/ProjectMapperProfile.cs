using System.Drawing;
using AutoMapper;
using Resource.UIModels;

namespace Resource.MapperProfiles;

public class ProjectMapperProfile : Profile {
    public ProjectMapperProfile()
    {
        CreateMap<Models.Project, ProjectUI>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.ProjectTypeId,
                opt => opt.MapFrom(src => src.ProjectTypeId)
            )
            .ForMember(
                dest => dest.OriginalProjectId,
                opt => opt.MapFrom(src => src.OriginalProjectId)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description)
            )
            .ForMember(
                dest => dest.Color,
                opt => opt.MapFrom(src => GetProjectColorFromStages(src.Stages))
            );
    }

    public static string DEFAULT_COLOR = "#23a5d588";

    private string GetProjectColorFromStages(IEnumerable<Models.Stage>? stages) {
        if(stages == null || stages.Count() == 0) return DEFAULT_COLOR;
        double meanDifference = stages.Select(stage => GetActExpDifference(stage)).Average(); // from -100 to 100
        return GetColorFromDifference(meanDifference);
    }

    private string GetColorFromDifference(double difference) {
        //r = 255 0 0
        //g = 0 255 0
        //y = 255 255 0

        int r, g;
        double bas = 255 * 2 * (1 / (1 + (Math.Abs(difference - 0.5) * 2)));
        r = (int)(bas * (1 - difference));
        g = (int)(bas * difference);


        return $"rgba({r}, {g}, 75, 0.5)";
    }

    /// <summary>
    /// Return difference between expected progress and actual progress for project stage
    /// </summary>
    /// <param name="stage">Project stage</param>
    /// <returns>Difference between expected progress and actual progress in range from 0(You have done nothing and there is a deadline) to 1(You finished this stage)</returns>
    private double GetActExpDifference(Models.Stage stage) {
        if(stage.Progress == 100) return 1;
        double timeSpent = (DateTime.Now - stage.CreationDate).TotalSeconds;
        double totalTime = (stage.Deadline - stage.CreationDate).TotalSeconds;
        double expectedProgress = timeSpent / totalTime;
        if(expectedProgress > 1) expectedProgress = 1;
        double actualProgress = stage.Progress / 100.0;
        double difference = Math.Pow(((actualProgress - expectedProgress) + 1) / 2, 0.7); //we use power for biasing color to green side
        return difference;
    }
}
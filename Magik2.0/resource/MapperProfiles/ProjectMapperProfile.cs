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

    private string GetProjectColorFromStages(IEnumerable<Models.Stage>? stages) {
        if(stages == null || stages.Count() == 0) return GetColorFromDifference(100);
        double meanDifference = stages.Select(stage => GetActExpDifference(stage)).Average(); // from -100 to 100
        return GetColorFromDifference(meanDifference);
    }

    private string GetColorFromDifference(double difference) {
        double normDifference = difference + 101; // to range from 1 to 201
        normDifference /= 201; // to range from 0 to ~0.99;
        
        //r = 255 0 0
        //g = 0 255 0
        //y = 255 255 0

        int r, g;
        if(normDifference > 0.5) {
            g = (int)(255 * (normDifference));
            r = (int)(255 * (1 - (normDifference - 0.5) * 2));
        }
        else {
            g = (int)(255 * ((normDifference - 0.5) * 2));
            r = (int)(255 * (1 - normDifference));
        }

        return $"rgba({r}, {g}, 75, 0.5)";
    }

    /// <summary>
    /// Return difference between expected progress and actual progress for project stage
    /// </summary>
    /// <param name="stage">Project stage</param>
    /// <returns>Difference between expected progress and actual progress in range from -100(You have done nothing and there is a deadline) to 100(You finished this stage)</returns>
    private double GetActExpDifference(Models.Stage stage) {
        if(stage.Progress == 100) return 100;
        double timeSpent = (DateTime.Now - stage.CreationDate).TotalSeconds;
        double totalTime = (stage.Deadline - stage.CreationDate).TotalSeconds;
        double expectedProgress = (timeSpent / totalTime) * 100;
        double actualProgress = stage.Progress;
        double difference = actualProgress - expectedProgress;
        return difference < -100 ? -100 : difference;
    }
}
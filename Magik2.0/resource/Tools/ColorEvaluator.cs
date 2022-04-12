using Resource.Models;

namespace Resource.Tools;

public static class ColorEvaluator {
    public static string DEFAULT_COLOR = "#23a5d588";

    public static string GetProjectColor(Project project) {
        if(project.Stages == null || project.Stages.Count() == 0) return DEFAULT_COLOR;
        var unfinishedStagesDifferences = project.Stages.Where(s => s.Progress != 100).Select(stage => GetActExpDifference(stage));
        if(unfinishedStagesDifferences.Count() == 0) return GetColorFromDifference(1);
        return GetColorFromDifference(unfinishedStagesDifferences.Average());
    }

    public static string GetStageColor(Stage stage) {
        return GetColorFromDifference(GetActExpDifference(stage));
    }

    private static string GetColorFromDifference(double difference) {
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
    private static double GetActExpDifference(Models.Stage stage) {
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
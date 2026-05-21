using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class AndroidBuild
{
    public static void PerformAndroidBuild()
    {
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new System.Exception("No enabled scenes found in Build Settings. Add at least one scene before building.");
        }

        var outputDirectory = "Builds/Android";
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var buildPath = Path.Combine(outputDirectory, "Cube.apk");
        var buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildOptions);
        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception($"Android build failed with result: {report.summary.result}");
        }
    }
}

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new System.Exception("No enabled scenes found in Build Settings.");
        }

        var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Builds/Android");

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        var buildPath = Path.Combine(outputDir, "Cube.apk");

        var buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        UnityEngine.Debug.Log("Starting Android Build at: " + buildPath);

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception(
                "Android build failed: " + report.summary.result
            );
        }

        UnityEngine.Debug.Log("Android build completed successfully!");
    }
}

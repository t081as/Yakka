using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using static Mjolnir.Build.PackageNameTasks;
using static Mjolnir.Build.VCS.GitVersionTasks;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class BuildTargets : NukeBuild
{
    public static int Main () => Execute<BuildTargets>(x => x.Default);

    [Parameter]
    readonly Configuration Configuration = Configuration.Debug;

    [Parameter(Name = "$CI_PIPELINE_IID")]
    readonly ulong Buildnumber = 0;

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath BuildDirectory => OutputDirectory / "build";
    AbsolutePath PublishDirectory => OutputDirectory / "publish";
    AbsolutePath CoverageDirectory => OutputDirectory / "coverage";
    AbsolutePath MainProjectFile => SourceDirectory / "Yakka" / "Yakka.csproj";

    readonly string GlobCoverageFiles = "**/TestResults/*/coverage.cobertura.xml";
    readonly string GlobTestResultFiles = "**/TestResults/TestResults.xml";

    string shortVersion = "0.0.0";
    string version = "0.0.0.0";
    string semanticVersion = "0.0.0+XXXXXXXX";

    Target Clean => _ => _
        .Executes(() =>
        {
            RootDirectory.GlobFiles(GlobCoverageFiles).ForEach(DeleteFile);
            RootDirectory.GlobFiles(GlobTestResultFiles).ForEach(DeleteFile);
            RootDirectory.GlobFiles("*.zip").ForEach(DeleteFile);

            EnsureCleanDirectory(BuildDirectory);
            EnsureCleanDirectory(PublishDirectory);
            EnsureCleanDirectory(CoverageDirectory);
            EnsureCleanDirectory(OutputDirectory);
            DotNetClean();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Version => _ => _
        .Executes(() =>
        {
            (string shortVersion, string version, string semanticVersion) = GetGitTagVersion(RootDirectory, Buildnumber);

            Logger.Info($"Version: {version}");
            Logger.Info($"Short Version: {shortVersion}");
            Logger.Info($"Semantic Version: {semanticVersion}");
            Logger.Info($"Buildnumber: {Buildnumber}");

            if (Configuration == Configuration.Release)
            {
                this.shortVersion = shortVersion;
                this.version = version;
                this.semanticVersion = semanticVersion;
            }
            else
            {
                Logger.Info("Debug build - skipping version");
            }
        });

    Target Compile => _ => _
        .DependsOn(Restore, Version)
        .Executes(() =>
        {
            if (Configuration == Configuration.Release)
            {
                DotNetBuild(_ => _
                    .SetProjectFile(MainProjectFile)
                    .SetOutputDirectory(BuildDirectory)
                    .SetConfiguration(Configuration)
                    .AddProperty("DebugType", "None")
                    .AddProperty("DebugSymbols", "false")
                    .SetVersion(semanticVersion)
                    .SetAssemblyVersion(version)
                    .SetFileVersion(version)
                    .EnableNoRestore());

                BuildDirectory.GlobFiles("*.dev.*").ForEach(DeleteFile);
                BuildDirectory.GlobFiles("*.deps.json").ForEach(DeleteFile); // dependencies will be shipped
                DeleteFile(BuildDirectory / "Yakka.xml"); // source code documentation xml
            }
            else
            {
                DotNetBuild(_ => _
                    .SetProjectFile(Solution)
                    .SetOutputDirectory(BuildDirectory)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore());
            }
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Debug)
        .Executes(() =>
        {
            string loggerConfiguration = $"junit;LogFilePath={OutputDirectory / "TestResults" / "TestResults.xml"};MethodFormat=Class;FailureBodyFormat=Verbose";

            DotNetTest(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetLogger(loggerConfiguration)
                .SetDataCollector("XPlat Code Coverage")
                .EnableNoRestore());

            if (IsWin)
            {
                ReportGenerator(_ => _
                    .SetToolPath(ToolPathResolver.GetPackageExecutable("ReportGenerator", "ReportGenerator.exe", null, "netcoreapp3.0"))
                    .SetReports(RootDirectory / GlobCoverageFiles)
                    .SetTargetDirectory(CoverageDirectory)
                    .SetFileFilters("-*Resources", "-*Form", "-*Control", "-*Settings", "-*Program", "-*View")
                    .SetReportTypes(ReportTypes.TextSummary, ReportTypes.Html));

                Logger.Info(File.ReadAllText(CoverageDirectory / "Summary.txt"));
            }
        });

    Target Publish => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .AddProperty("DebugType", "None")
                .AddProperty("DebugSymbols", "false")
                .SetProject(MainProjectFile)
                .SetOutput(PublishDirectory / "win-x64")
                .EnableSelfContained()
                .AddProperty("PublishSingleFile", "true")
                .AddProperty("PublishTrimmed", "true")
                .SetVersion(semanticVersion)
                .SetAssemblyVersion(version)
                .SetFileVersion(version)
                .SetRuntime("win-x64"));

            CopyFile(RootDirectory / "AUTHORS.txt", PublishDirectory / "win-x64" / "AUTHORS.txt");
            CopyFile(RootDirectory / "CHANGELOG.md", PublishDirectory / "win-x64" / "CHANGELOG.txt");
            CopyFile(RootDirectory / "LICENSE.txt", PublishDirectory / "win-x64" / "LICENSE.txt");
            CopyFile(RootDirectory / "NOTICE.txt", PublishDirectory / "win-x64" / "NOTICE.txt");

            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .AddProperty("DebugType", "None")
                .AddProperty("DebugSymbols", "false")
                .SetProject(MainProjectFile)
                .SetOutput(PublishDirectory / "win-x86")
                .EnableSelfContained()
                .AddProperty("PublishSingleFile", "true")
                .AddProperty("PublishTrimmed", "true")
                .SetVersion(semanticVersion)
                .SetAssemblyVersion(version)
                .SetFileVersion(version)
                .SetRuntime("win-x86"));

            CopyFile(RootDirectory / "AUTHORS.txt", PublishDirectory / "win-x86" / "AUTHORS.txt");
            CopyFile(RootDirectory / "CHANGELOG.md", PublishDirectory / "win-x86" / "CHANGELOG.txt");
            CopyFile(RootDirectory / "LICENSE.txt", PublishDirectory / "win-x86" / "LICENSE.txt");
            CopyFile(RootDirectory / "NOTICE.txt", PublishDirectory / "win-x86" / "NOTICE.txt");

            string versionLabel = semanticVersion.Contains(DevMarker) switch
            {
                false => shortVersion,
                true => semanticVersion
            };

            CompressionTasks.CompressZip(
                BuildDirectory,
                RootDirectory / GenerateBinaryPackageName("Yakka", versionLabel, Mjolnir.Build.OperatingSystem.Windows, Mjolnir.Build.Architecture.AnyCpu) + ".zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);

            CompressionTasks.CompressZip(
                PublishDirectory / "win-x64",
                RootDirectory / GenerateBinaryPackageName("Yakka", versionLabel, Mjolnir.Build.OperatingSystem.Windows, Mjolnir.Build.Architecture.X64) + ".zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);

            CompressionTasks.CompressZip(
                PublishDirectory / "win-x86",
                RootDirectory / GenerateBinaryPackageName("Yakka", versionLabel, Mjolnir.Build.OperatingSystem.Windows, Mjolnir.Build.Architecture.X86) + ".zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);
        });

    Target Default => _ => _
        .DependsOn(Publish, Test);

}

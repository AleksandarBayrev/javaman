using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using javaman;
using JavaMan.Constants;
using JavaMan.Enums;
using JavaMan.Handlers;
using JavaMan.Interfaces;
using JavaMan.SdkProviders;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var pathVariable = (string)(Environment.GetEnvironmentVariables()[EnvironmentVariablesKeys.PATHUnix] ?? Environment.GetEnvironmentVariables()[EnvironmentVariablesKeys.PATHWindows] ?? "");
        var javaHomeVariable = (string)(Environment.GetEnvironmentVariables()[EnvironmentVariablesKeys.JAVA_HOME] ?? "");
        if (pathVariable == null || javaHomeVariable == null)
        {
            return;
        }

        var pathLinux = Environment.OSVersion.Platform == PlatformID.Unix ? pathVariable.Split(":") : Enumerable.Empty<string>();
        var javaHomeLinux = Environment.OSVersion.Platform == PlatformID.Unix ? javaHomeVariable.Split(":") : Enumerable.Empty<string>();
        var pathWindows = Environment.OSVersion.Platform == PlatformID.Win32NT ? pathVariable.Split(";") : Enumerable.Empty<string>();
        var javaHomeWindows = Environment.OSVersion.Platform == PlatformID.Win32NT ? javaHomeVariable.Split(";") : Enumerable.Empty<string>();
        ISdkProvider eclipseTemurinSdkProvider = new EclipseTemurinSdkProvider();
        CommandParser.AddCommandHandler(CommandType.InstallJava, new JavaInstallCommandHandler(eclipseTemurinSdkProvider));
        CommandParser.AddCommandHandler(CommandType.ListCandidates, new JavaListCandidatesCommandHandler());
        CommandParser.AddCommandHandler(CommandType.Help, new HelpCommandHandler());
        if (args.Length == 0)
        {
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(javaHomeVariable));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathVariable));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathLinux));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(javaHomeLinux));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(javaHomeWindows));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathWindows));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathLinux));
            await CommandParser.Execute(["help"]);
        }
        else
        {
            await CommandParser.Execute(args);
        }
    }
}
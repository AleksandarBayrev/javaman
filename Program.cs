using System;
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
        var pathVariable = (string)(Environment.GetEnvironmentVariables()[EnvironmentVariablesKeys.PATH] ?? "");
        var javaHomeVariable = (string)(Environment.GetEnvironmentVariables()[EnvironmentVariablesKeys.JAVA_HOME] ?? "");
        if (pathVariable == null || javaHomeVariable == null)
        {
            return;
        }

        var pathLinux = Environment.OSVersion.Platform == PlatformID.Unix ? pathVariable.Split(":") : Enumerable.Empty<string>();
        var javaHomeLinux = Environment.OSVersion.Platform == PlatformID.Unix ? javaHomeVariable.Split(":") : Enumerable.Empty<string>();
        if (args.Length == 0)
        {
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathLinux));
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(javaHomeLinux));
            await Console.Out.WriteLineAsync("Welcome to javaman!");
            await Console.Out.WriteLineAsync("Available commands:");
            await Console.Out.WriteLineAsync("install <candidate> <version>");
            await Console.Out.WriteLineAsync("list-candidates");
        }
        else
        {
            ISdkProvider eclipseTemurinSdkProvider = new EclipseTemurinSdkProvider();
            CommandParser.AddCommandHandler(CommandType.InstallJava, new JavaInstallCommandHandler(eclipseTemurinSdkProvider));
            await CommandParser.Execute(args);
        }
    }
}
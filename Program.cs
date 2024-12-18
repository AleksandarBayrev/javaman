using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var data = (string)(Environment.GetEnvironmentVariables()["PATH"] ?? "");
        if (data == null)
        {
            return;
        }
        
        var pathLinux = Environment.OSVersion.Platform == PlatformID.Unix ?  data.Split(":") : Enumerable.Empty<string>();
        await Console.Out.WriteLineAsync(JsonSerializer.Serialize(pathLinux));
        await Console.Out.WriteLineAsync("Welcome to javaman!");
        await Console.Out.WriteLineAsync("Available commands:");
        await Console.Out.WriteLineAsync("install <candidate> <version>");
        await Console.Out.WriteLineAsync("list-candidates");
    }
}
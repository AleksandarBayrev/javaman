using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using JavaMan.Enums;
using JavaMan.Interfaces;
using JavaMan.Types;

namespace JavaMan.Handlers
{
    public class JavaInstallCommandHandler : ICommandHandler
    {
        private readonly ISdkProvider _sdkProvider;

        public JavaInstallCommandHandler(ISdkProvider sdkProvider)
        {
            _sdkProvider = sdkProvider;
        }
        public async Task<TaskResult> Execute(string[] args)
        {
            var version = args[2];
            using (var client = new HttpClient())
            {
                try
                {
                    await Console.Out.WriteLineAsync($"Starting downloading Java {version}");
                    var response = await client.GetByteArrayAsync(_sdkProvider.GetUrl(version));
                    await File.WriteAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "sdks", $"java-{version}"), response);
                    return new TaskResult($"Successfully downloaded Java {version}!", StatusCodes.Success);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                    return new TaskResult($"Failed to download Java {version}!", StatusCodes.BadRequest);
                }
            }
        }
    }
}
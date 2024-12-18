using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using JavaMan.Enums;
using JavaMan.Interfaces;
using JavaMan.Types;

namespace JavaMan.Handlers
{
    public class InstallCommandHandler : ICommandHandler
    {
        private readonly ISdkProvider _sdkProvider;

        public InstallCommandHandler(ISdkProvider sdkProvider)
        {
            _sdkProvider = sdkProvider;
        }
        public async Task<TaskResult> Execute(string[] args)
        {
            var version = args[0];
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetByteArrayAsync(_sdkProvider.GetUrl(version));
                    await File.WriteAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "sdks", $"java-{version}"), response);
                    return new TaskResult($"Successfully downloaded Java {version}!", StatusCodes.Success);
                }
                catch (Exception ex)
                {
                    return new TaskResult($"Failed to download Java {version}!", StatusCodes.BadRequest);
                }
            }
        }
    }
}
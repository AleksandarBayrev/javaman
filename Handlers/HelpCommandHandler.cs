using System;
using System.Threading.Tasks;
using JavaMan.Interfaces;
using JavaMan.Types;

namespace JavaMan.Handlers
{
    public class HelpCommandHandler : ICommandHandler
    {
        public async Task<TaskResult> Execute(string[] args)
        {
            await Console.Out.WriteLineAsync("Welcome to javaman!");
            await Console.Out.WriteLineAsync("Available commands:");
            await Console.Out.WriteLineAsync("install <candidate> <version>");
            await Console.Out.WriteLineAsync("list-candidates");
            return new TaskResult("", Enums.StatusCodes.Success);
        }
    }
}
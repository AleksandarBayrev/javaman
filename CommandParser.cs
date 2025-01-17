using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaMan.Enums;
using JavaMan.Interfaces;
using JavaMan.Types;

namespace javaman
{
    public static class CommandParser
    {
        private static IDictionary<CommandType, ICommandHandler> _commands;
        static CommandParser()
        {
            _commands = new Dictionary<CommandType, ICommandHandler>();
        }
        public static void AddCommandHandler(CommandType commandType, ICommandHandler commandHandler)
        {
            _commands.Add(commandType, commandHandler);
        }
        public static async Task<TaskResult> Execute(string[] args)
        {
            try
            {
                await Console.Out.WriteLineAsync($"Getting handler {args[0]}");
                _commands.TryGetValue(GetCommandType(args), out ICommandHandler? handler);
                await Console.Out.WriteLineAsync($"Executing handler {args[0]}");
                return await handler.Execute(args);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Command {args[0]} not found! Ex: {ex.Message}");
                return new TaskResult($"Command {args[0]} not found!", StatusCodes.CommandNotFound);
            }
        }
        private static CommandType GetCommandType(string[] args)
        {
            var command = $"{string.Join(' ', args)}".Trim();
            if (command.Contains("install java"))
            {
                return CommandType.InstallJava;
            }
            if (command.Contains("help"))
            {
                return CommandType.Help;

            }
            if (command.Contains("list-candidates"))
            {
                return CommandType.ListCandidates;

            }
            throw new Exception("Command not found!");
        }
    }
}
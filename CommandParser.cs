using System.Collections.Generic;
using System.Threading.Tasks;
using JavaMan.Enums;
using JavaMan.Interfaces;

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
        public static async Task Execute(CommandType commandType, string[] args)
        {
            _commands.TryGetValue(commandType, out ICommandHandler? handler);
            if (handler != null)
            {
                await handler.Execute(args);
            }
        }
    }
}
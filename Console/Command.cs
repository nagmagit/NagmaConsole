using System;

namespace Nagma
{
    public struct Command
    {
        /// <summary>
        /// The name of the command.
        /// </summary>
        public string CommandName { get; }
        /// <summary>
        /// This is the description of the command can will be shown with the command "availablecommands".
        /// </summary>
        public string CommandDescription { get; }
        /// <summary>
        /// The method that will respond to the command.
        /// </summary>
        public Action<string[]> CommandAction { get; }

        /// <summary>
        /// Creates a new command. Its name will be equal to its method name.
        /// </summary>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public Command(Action<string[]> commandAction)
        {
            CommandName = commandAction.Method.Name.Trim(' ');
            CommandAction = commandAction;
            CommandDescription = String.Empty;
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="commandName">The name that the command will respond to.</param>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public Command(string commandName, Action<string[]> commandAction)
        {
            CommandName = commandName;
            CommandAction = commandAction;
            CommandDescription = String.Empty;
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="commandName">The name that the command will respond to.</param>
        /// <param name="commandDescription">This is the description of the command that will be shown with the command "availablecommands".</param>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public Command(string commandName, string commandDescription, Action<string[]> commandAction)
        {
            CommandName = commandName;
            CommandAction = commandAction;
            CommandDescription = commandDescription;
        }
    }
}

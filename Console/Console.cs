using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nagma
{
    public partial class Console
    {
        public const string CONSOLE_VER = "1.0.0";

        public Dictionary<string, Command> CommandsList { get; } = new Dictionary<string, Command>();
        /// <summary>
        /// This is the journal itself. It's not recommended to write to it directly, instead use the Console.Log() method.
        /// </summary>
        public List<IJournalEntry> Journal { get; } = new List<IJournalEntry>();

        /// <summary>
        /// Event raised when something is logged into the journal.
        /// </summary>
        public event JournalChangedEventHandler JournalLogged;
        /// <summary>
        /// Event raised when the "clear" command or the Console.Clear() method is called.
        /// </summary>
        public event JournalWhipedEventHandler JournalWhiped;

        /// <summary>
        /// Creates an instance of the console.
        /// </summary>
        public Console()
        {
            AddStandardCommands();
        }
        /// <summary>
        /// Creates an instance of the console.
        /// </summary>
        /// <param name="standardCommands">Should the standard commands be added?</param>
        public Console(bool standardCommands)
        {
            if (standardCommands) AddStandardCommands();
        }

        /// <summary>
        /// Creates a new command. Its name will be equal to the method's name.
        /// </summary>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public void AddCommand(Action<string[]> commandMethod)
        {
            if (!CommandsList.ContainsKey(commandMethod.Method.Name))
            {
                var command = new Command(commandMethod);
                CommandsList.Add(command.CommandName, command);
            }
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="commandName">The name that the command will respond to.</param>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public void AddCommand(string commandAlias, Action<string[]> commandMethod)
        {
            if (!CommandsList.ContainsKey(commandAlias))
            {
                var command = new Command(commandAlias, commandMethod);
                CommandsList.Add(commandAlias, command);
            }
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="commandName">The name that the command will respond to.</param>
        /// <param name="commandDescription">This is the description of the command that will be shown with the command "availablecommands".</param>
        /// <param name="commandAction">The method that will respond to the command.</param>
        public void AddCommand(string commandAlias, string commandDescription, Action<string[]> commandMethod)
        {
            if (!CommandsList.ContainsKey(commandAlias))
            {
                var command = new Command(commandAlias, commandDescription, commandMethod);
                CommandsList.Add(commandAlias, command);
            }
            else
            {
                throw new ArgumentException("The alias for this command is already in use.");
            }
        }
        /// <summary>
        /// Removes a command.
        /// </summary>
        /// <param name="commandName">The alias of the command.</param>
        public void RemoveCommand(string commandAlias)
        {
            if (CommandsList.ContainsKey(commandAlias))
            {
                CommandsList.Remove(commandAlias);
            }
        }

        #region Log commands
        /// <summary>
        /// Call this method if you want to log something into the journal.
        /// </summary>
        public void Log(string text)
        {
            var entry = new JournalLog(text);

            Journal.Add(entry);
            JournalLogged.Invoke(new JournalChangedEventArgs(entry));
        }
        public void LogError(string text)
        {
            var entry = new JournalError(text);

            Journal.Add(entry);
            JournalLogged.Invoke(new JournalChangedEventArgs(entry));
        }
        public void LogWarn(string text)
        {
            var entry = new JournalWarn(text);

            Journal.Add(entry);
            JournalLogged.Invoke(new JournalChangedEventArgs(entry));
        }
        #endregion

        /// <summary>
        /// Where the magic happens. Send a command and its arguments as a string here.
        /// </summary>
        public void Execute(string args)
        {
            var parameters = args.SmartDivision();

            if (String.IsNullOrEmpty(args.Trim(' ', '\t'))) return;

            if (CommandsList.ContainsKey(parameters[0]))
            {
                var command = CommandsList[parameters[0]];

                try
                {
                    command.CommandAction.Invoke(parameters);
                }
                catch (Exception e)
                {
                    LogError(String.Format("The command outputted an error: \"{0}\"", e.Message));
                }
            }
            else
            {
                LogError("Command not found.");
            }
        }

        private void AddStandardCommands()
        {
            // Add the standard commands
            AddCommand("help", "Shows all the available commands.", ShowAllCommands);
            AddCommand("consoleversion", "Shows the current version of the Console assembly.", ConsoleVersion);
            AddCommand("clear", "Clears the console's journal.", Clear);
            AddCommand("echo", "Outputs the given text.", Echo);
        }
    }
}
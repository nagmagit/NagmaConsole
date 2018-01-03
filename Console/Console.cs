using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nagma
{
    public class Console
    {
        public const string CONSOLE_VER = "0.1.0";

        public Dictionary<string, Command> CommandsList { get; } = new Dictionary<string, Command>();
        /// <summary>
        /// This is the journal itself. It's not recommended to write to it directly, instead use the Console.Log() method.
        /// </summary>
        public List<string> Journal { get; } = new List<string>();

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
        /// Creates a new command. Its name will be equal to its method name.
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
            if (!CommandsList.ContainsKey(commandMethod.Method.Name))
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
            if (!CommandsList.ContainsKey(commandMethod.Method.Name))
            {
                var command = new Command(commandAlias, commandDescription, commandMethod);
                CommandsList.Add(commandAlias, command);

            }
        }

        #region Log commands
        /// <summary>
        /// Call this method if you want to log something into the journal.
        /// </summary>
        public void Log(string text)
        {
            Journal.Add(text);
            JournalLogged.Invoke(new JournalChangedEventArgs(text));
        }
        #endregion

        /// <summary>
        /// Where the magic happens. Send a command and its arguments as a string here.
        /// </summary>
        public void Execute(string args)
        {
            // TODO: Fix parameter splitting to NOT split "quotation marks"
            var parameters = args.Split(' ');

            if (CommandsList.ContainsKey(parameters[0]))
            {
                var command = CommandsList[parameters[0]];

                try
                {
                    command.CommandAction.Invoke(parameters);
                }
                catch (Exception e)
                {
                    Log(String.Format("The command outputted an error: \"{0}\"", e.Message));
                }
            }
        }

        #region Standard commands
        private void AddStandardCommands()
        {
            // Add the standard commands
            AddCommand("availablecommands", "Shows all the available commands.", ShowAllCommands);
            AddCommand("consoleversion", "Shows the current version of the Console assembly.", ConsoleVersion);
            AddCommand("clear", "Clears the console's journal.", Clear);
            AddCommand("echo", "Outputs the given text.", Echo);
        }

        private void ShowAllCommands(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\nAvailable commands:\n");

            var longestCommandLength = CommandsList.Max(e => e.Key.Length);

            foreach(var command in CommandsList)
            {
                var nameToOut = command.Key;

                for (int i = command.Key.Length; i < longestCommandLength; i++)
                    nameToOut += " ";

                if (String.IsNullOrEmpty(command.Value.CommandDescription))
                {
                    sb.AppendLine(String.Format("{0} No description.", nameToOut));
                }
                else
                {
                    sb.AppendLine(String.Format("{0} {1}", nameToOut, command.Value.CommandDescription));
                }
            }

            Log(sb.ToString());
        }

        private void ConsoleVersion(string[] args)
        {
            Log(String.Format("Current version: ", CONSOLE_VER));
        }

        private void Echo(string[] args)
        {
            Log(String.Join(" ", args.SubArray(1, args.Length - 1)));
        }

        public void Clear(string[] args)
        {
            Journal.Clear();
            JournalWhiped.Invoke(new JournalWhipedEventArgs());
        }
        #endregion
    }
}
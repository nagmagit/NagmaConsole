using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nagma
{
    public partial class Console
    {
        private void ShowAllCommands(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\nAvailable commands:\n");

            var longestCommandLength = CommandsList.Max(e => e.Key.Length);

            foreach (var command in CommandsList)
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
            Log(String.Format("Current version: {0}", CONSOLE_VER));
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
    }
}

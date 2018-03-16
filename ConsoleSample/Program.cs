using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nagma;

namespace ConsoleSample
{
    class Program
    {
        static Nagma.Console console;

        static void Main(string[] args)
        {
            // Create a new instance of the console
            // The standard commands can be ommitted by passing a "false"
            // to the "standardCommands" parameter of the constructor
            console = new Nagma.Console();

            // Subscribe to the events.
            console.JournalLogged += Console_JournalLogged; // This event is raised when Console.Log is called
            console.JournalWhiped += Console_JournalWhiped; // This event is raised by the "clear" command

            // Add some commands
            console.AddCommand(SayBananaXTimes);
            console.AddCommand("repeat", SayRepeat);
            console.AddCommand("saybanana", "Just outputs \"banana\".", SayBanana);

            console.Execute("help"); // We execute the built-in command "help"

            while (true)
            {
                System.Console.Write("> "); // Create a good ol' cursor
                var input = System.Console.ReadLine();

                console.Execute(input); // Enter commands
            }
        }

        private static void Console_JournalWhiped(JournalWhipedEventArgs e)
        {
            System.Console.Clear();
        }

        private static void Console_JournalLogged(JournalChangedEventArgs e)
        {
            var logType = String.Empty; // 
            var logColor = ConsoleColor.White;

            // Here we check what type of JournalEntry is e and write a prefix and a color accordingly.
            if (e.Entry is JournalError)
            {
                logType = "ERR";
                logColor = ConsoleColor.Red;
            }
            else if (e.Entry is JournalWarn)
            {
                logType = "WRN";
                logColor = ConsoleColor.Yellow;
            }
            else
            {
                logType = "LOG";
            }

            // This time we call the real console to output
            WriteColored(String.Format("[{0}] {1} - {2}", logType, e.Entry.TimeStamp, e.Entry.Message), logColor);
        }

        public static void WriteColored(string text, ConsoleColor color)
        {
            // This just saves the current forecolor of the System console and sets it back after writing the text.
            var prevColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = prevColor;
        }

        // Test commands
        public static void SayBananaXTimes(string[] args)
        {
            for (int i = 0; i < Int32.Parse(args[1]); i++) // Here, an error might occur. Try entering a non numeric character
            {
                console.Log("banana");
            }
        }
        public static void SayRepeat(string[] args)
        {
            for (int i = 0; i < Int32.Parse(args[2]); i++) // Here, an error might occur. Try entering a non numeric character
            {
                console.Log(args[1]);
            }
        }
        public static void SayBanana(string[] args)
        {
            console.Log("banana");
        }
    }
}

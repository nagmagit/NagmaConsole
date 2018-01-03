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
            console = new Nagma.Console(); // We have to instanciate the console

            // We subscribe to the events.
            console.JournalLogged += Console_JournalLogged; // This event is raised when Console.Log is called
            console.JournalWhiped += Console_JournalWhiped; // This event is raised by the "clear" command

            // We add some commands
            console.AddCommand(SayBananaXTimes);
            console.AddCommand("shrug", SayShrugFace);
            console.AddCommand("saybanana", "Just outputs \"banana\".", SayBanana);

            console.Execute("availablecommands"); // We execute the built-in command "availablecommands"

            while (true)
            {
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
            // This time we call the real console to output
            System.Console.WriteLine(String.Format("{0} - {1}", e.TimeStamp, e.Logged));
        }

        public static void SayBananaXTimes(string[] args)
        {
            for (int i = 0; i < Int32.Parse(args[1]); i++) // Here, an error might occur. Try entering a non numeric character
            {
                console.Log("banana");
            }
            
        }
        public static void SayShrugFace(string[] args)
        {
            console.Log(@"¯\_(ツ)_/¯");
        }
        public static void SayBanana(string[] args)
        {
            console.Log("banana");
        }
    }
}

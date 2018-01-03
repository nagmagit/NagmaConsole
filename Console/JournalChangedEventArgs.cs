using System;

namespace Nagma
{
    public delegate void JournalChangedEventHandler(JournalChangedEventArgs e);
    public delegate void JournalWhipedEventHandler(JournalWhipedEventArgs e);

    public class JournalChangedEventArgs : EventArgs
    {
        // The text inserted to the journal.
        public string Logged { get; }
        // The moment when the text was logged (localized time).
        public DateTime TimeStamp { get; }

        public JournalChangedEventArgs(string text)
        {
            Logged = text;
            TimeStamp = DateTime.Now;
        }
    }

    public class JournalWhipedEventArgs : EventArgs
    {
        // The moment when the journal was whiped (localized time).
        public DateTime TimeStamp { get; }

        public JournalWhipedEventArgs()
        {
            TimeStamp = DateTime.Now;
        }
    }
}

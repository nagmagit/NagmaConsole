using System;

namespace Nagma
{
    public delegate void JournalChangedEventHandler(JournalChangedEventArgs e);
    public delegate void JournalWhipedEventHandler(JournalWhipedEventArgs e);

    public class JournalChangedEventArgs : EventArgs
    {
        public IJournalEntry Entry { get; }

        public JournalChangedEventArgs(IJournalEntry log)
        {
            Entry = log;
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
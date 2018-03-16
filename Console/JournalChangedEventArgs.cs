using System;

namespace Nagma
{
    public delegate void JournalChangedEventHandler(JournalChangedEventArgs e);
    public delegate void JournalWhipedEventHandler(JournalWhipedEventArgs e);

    public class JournalChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The entry added to the journal
        /// </summary>
        public IJournalEntry Entry { get; }

        public JournalChangedEventArgs(IJournalEntry log)
        {
            Entry = log;
        }
    }

    public class JournalWhipedEventArgs : EventArgs
    {
        /// <summary>
        /// The moment when the journal was whiped (localized time).
        /// </summary>
        public DateTime TimeStamp { get; }

        public JournalWhipedEventArgs()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
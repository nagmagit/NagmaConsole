using System;

namespace Nagma
{
    public interface IJournalEntry
    {
        DateTime TimeStamp { get; }
        string Message { get; }
    }

    public struct JournalLog : IJournalEntry
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }

        public JournalLog(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }
    }
    public struct JournalError : IJournalEntry
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }

        public JournalError(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }
    }
    public struct JournalWarn : IJournalEntry
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }

        public JournalWarn(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }
    }
}
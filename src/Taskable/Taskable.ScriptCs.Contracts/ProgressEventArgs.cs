using System;

namespace TaskableScriptCs.Contracts
{
    public class ProgressEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public ProgressType Type { get; set; }

        public ProgressEventArgs(string message, ProgressType type)
        {
            this.Message = message;
            this.Type = type;
        }
    }

    public enum ProgressType
    {
        Log,
        Error
    }
}

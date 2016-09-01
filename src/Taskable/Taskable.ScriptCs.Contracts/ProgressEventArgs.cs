using System;

namespace TaskableScriptCs.Contracts
{
    public class ProgressEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ProgressEventArgs(string message)
        {
            this.Message = message;
        }
    }
}

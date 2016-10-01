using System;

namespace TaskableScriptCs.Contracts
{
    public static class TaskProgress
    {
        private static object rootObj = new object();

        public static event EventHandler<ProgressEventArgs> MessageReceived;

        public static void Report(string message)
        {
            lock (rootObj)
            {
                OnMessageReceived(message, ProgressType.Log);
            }
        }

        public static void ReportError(string message)
        {
            lock (rootObj)
            {
                OnMessageReceived(message, ProgressType.Error);
            }
        }

        private static void OnMessageReceived(string message, ProgressType type)
        {
            MessageReceived?.Invoke(null, new ProgressEventArgs(message, type));
        }
    }
}

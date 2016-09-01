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
                OnMessageReceived(message);
            }
        }

        private static void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(null, new ProgressEventArgs(message));
        }
    }
}

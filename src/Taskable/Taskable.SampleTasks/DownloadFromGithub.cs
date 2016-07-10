using System;
using TaskableScriptCs.Contracts;

namespace TaskableSampleTasks
{
    public class DownloadFromGithub : ISimpleTask
    {
        public string Pattern
        {
            get
            {
                return "download {} to {}";
            }
        }

        Action<string[]> ISimpleTask.Stuff
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

using System;
using TaskableScriptCs.Contracts;

namespace TaskableSampleTasks
{
    public class DownloadFromGithub : ISimpleTask
    {
        public string Example
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "Download from github";
            }
        }

        public string Pattern
        {
            get
            {
                return "download {} to {}";
            }
        }

        public Action<string> Stuff
        {
            get
            {
                throw new NotImplementedException();
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

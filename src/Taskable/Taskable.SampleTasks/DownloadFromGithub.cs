using System;
using System.Net;
using TaskableScriptCs.Contracts;

namespace TaskableSampleTasks
{
    [TaskName("Download Task")]
    [TaskExample(@"git-download https://github.com/karthik25/sblog.net/archive/master.zip to d:\temp\GitDownloads")]
    public class GitDownloadTask : ISimpleTask
    {
        public string Pattern
        {
            get
            {
                return "git-download {} to {}";
            }
        }

        public Action<string[]> Stuff
        {
            get
            {
                return parameters =>
                {
                    var fileUrl = parameters[0];
                    var destinationDirectory = parameters[1];
                    TaskProgress.Report("Copying " + fileUrl + " to " + destinationDirectory);
                    try
                    {
                        var client = new WebClient();
                        client.DownloadFile(fileUrl, destinationDirectory + @"\master.zip");
                    }
                    catch (Exception ex)
                    {
                        TaskProgress.ReportError(ex.Message.ToString());
                    }
                };
            }
        }
    }
}

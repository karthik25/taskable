# Read Me

Taskable is an attempt to do complex tasks with english like statements. Taskable can be used as scriptcs script pack or a WPF application.

Taskable was highly influenced by bau. I was using Bau for a few things and something I missed the most was the ability to pass parameters. I did submit a PR for it. But it still hasn't been merged, prompting me to think of something else and I thought of this - taskable! At that point I had rarely used powershell and when I did, I was surprsied to see how similar my thought process was to powershell!That's one reason I would say taskable is the Poor man's PowerShell :-)

## Building

### WPF application

Just build the solution and hit `Ctrl + F5` to launch the application!

### scriptcs package

First step is to create the scriptcs package. To create this, navigate to the `TaskableScriptPack` project's directory. The `.nuspec` file is already present in here. Enter the following to create the package:

```
nuget pack TaskableScriptPack.csproj -IncludeReferencedProjects
```

Once this is done, you need to inform nuget about the presence of this package. To do this go to `%AppData\Roaming\NuGet\NuGet.config` and add a new key to indicate the location of this package.

Next step is to add an entry for Taskable in your `scriptcs_packages.config`.

```
<package id="TaskableScriptPack" version="1.0.0" targetFramework="net46" />
```

After this step, you are ready to install the package and you can do this by running:

```
scriptcs -install
```

in your scriptcs home.

Here is the simplest taskable task for your perusal, lets call it `hello_taskable.csx`!

```csharp
var taskable = Require<Taskable>();

[TaskName("Echo Task")]
[TaskExample("echo hello")]
[TaskExample(@"echo ""hello again""")]
public class EchoTask : ISimpleTask
{
    public string Pattern
    {
        get
        {
            return "echo {}";
        }
    }

    public Action<string[]> Stuff
    {
        get
        {
            return parameters =>
            {
                Console.WriteLine("Echo: " + parameters[0]);
            };
        }
    }
}

taskable.RegisterTask(new EchoTask());
taskable.WaitForCommands();
```

Here is yet another task. This task downloads the identified file that is available to the public:

```csharp
using System.Net;

var taskable = Require<Taskable>();

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
                Console.WriteLine("Copying " + fileUrl + " to " + destinationDirectory);
                try{
                    WebClient client = new WebClient();
                    client.DownloadFile(fileUrl, destinationDirectory + @"\master.zip");
                }
                catch(Exception ex){
                    Console.WriteLine(ex.ToString());
                }
            };
        }
    }
}

taskable.RegisterTask(new GitDownloadTask());
taskable.WaitForCommands();
```

To start using taskable, you can do the following:

```
> scriptcs -- hello_taskable.csx
```

Now you can start calling the registered commands! For example, the following would run the echo task:

```
> echo hello
```

This read me file is also a work-in-progress. So come back later and check!

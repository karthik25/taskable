using System.Collections.Generic;
using System.Reflection;

namespace TaskableRoslyn.Contracts
{
    public class AssemblyResult
    {
        public AssemblyResult()
        {
            this.Assemblies = new List<Assembly>();
            this.CompileErrors = new List<string>();
        }

        public List<Assembly> Assemblies { get; set; }
        public List<string> CompileErrors { get; set; }
    }
}

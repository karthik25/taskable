using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Taskable.Core.Concrete
{
    public class ParameterData
    {
        public Regex Regex { get; set; }
        public List<int> Positions { get; set; }        
    }
}

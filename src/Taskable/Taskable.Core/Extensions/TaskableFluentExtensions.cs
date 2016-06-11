using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Taskable.Core.Concrete;

namespace Taskable.Core.Extensions
{
    public static class TaskableFluentExtensions
    {
        public static ParameterData Parse(this string src)
        {
            var builder = new StringBuilder();
            var items = src.SplitWithSpace().ToList();
            var postions = new List<int>();
            for (var i = 0; i < items.Count(); i++)
            {
                switch (items[i])
                {
                    case " ":
                        builder.Append("([ ])");
                        break;
                    case "{}":
                        {
                            builder.Append(@"(\S+)");
                            postions.Add(i);
                        }
                        break;
                    default:
                        builder.Append(string.Format("({0})", items[i]));
                        break;
                }
            }
            return new ParameterData { Regex = new Regex(builder.ToString()), Positions = postions };
        }

        public static IEnumerable<string> GetParameters(this ComputedTask computedTask, string command)
        {
            var matches = computedTask.Data.Regex.Match(command);
            foreach (var position in computedTask.Data.Positions)
                yield return matches.Groups[(position + 1)].Value;
        }

        public static IEnumerable<string> SplitWithSpace(this string src)
        {
            var splits = src.Split(new[] { ' ' });
            return
                splits.Select(
                    (s, i) => new { Tokens = i < (splits.Count() - 1) ? new List<string> { s, " " } : new List<string> { s } })
                      .SelectMany(s => s.Tokens);
        }

        public static string Shift(this string src)
        {
            var splits = src.Split(new[] { ' ' });
            return splits.First();
        }
    }
}

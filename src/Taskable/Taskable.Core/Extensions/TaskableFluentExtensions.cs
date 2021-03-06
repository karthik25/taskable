﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TaskableCore.Concrete;
using TaskableScriptCs.Contracts;

namespace TaskableCore.Extensions
{
    public static class TaskableFluentExtensions
    {
        public static string GetName(this ISimpleTask simpleTask)
        {
            var type = simpleTask.GetType();
            var attribute = type.GetCustomAttributes(false).OfType<TaskNameAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                return attribute.Name;
            }
            return type.Name;
        }

        public static IEnumerable<string> GetExamples(this ISimpleTask simpleTask)
        {
            var type = simpleTask.GetType();
            var attributes = type.GetCustomAttributes(false).OfType<TaskExampleAttribute>();
            if (attributes.Any())
            {
                return attributes.Select(a => a.Example);
            }
            return null;
        }

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
                            builder.Append(@"("".*?""|\S+)");
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
            var acceptedEnclosers = new char[] { '\'', '\"' };
            var matches = computedTask.Data.Regex.Match(command);
            foreach (var position in computedTask.Data.Positions)
                yield return matches.Groups[(position + 1)]
                                    .Value
                                    .TrimStart(acceptedEnclosers)
                                    .TrimEnd(acceptedEnclosers);
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

        public static string[] GetCommandParameters(this string src)
        {
            var splits = src.Split(new[] { ' ' });
            return splits.Skip(1).ToArray();
        }
    }
}

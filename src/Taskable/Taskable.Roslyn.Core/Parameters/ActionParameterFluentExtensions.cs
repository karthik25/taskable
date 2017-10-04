using System;

namespace TaskableRoslynCore.Parameters
{
    public static class ActionParameterFluentExtensions
    {
        public static T Bind<T>(this string[] args)
            where T : class
        {
            throw new NotImplementedException();
        }

        public static object Bind(this Type type, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Linq;
using TaskableScriptCs.Contracts;

namespace Taskable.Core.Parameters
{
    public static class ActionParameterFluentExtensions
    {
        public static TConcrete Bind<TConcrete>(this string[] args)
            where TConcrete : class
        {
            return (TConcrete) Bind(typeof(TConcrete), args);
        }

        public static object Bind(this Type type, string[] args)
        {
            var propertiesWithParameterAttr = type.GetProperties()
                                                  .Where(p => p.GetCustomAttributes(false)
                                                               .OfType<ParameterIndexAttribute>()
                                                               .Any());
            if (propertiesWithParameterAttr.Count() != args.Length)
                throw new Exception("Count of properties with the ParameterIndexAttribute does not match the count for the arguments array");
            var instance = Activator.CreateInstance(type);
            for (int i = 0; i < args.Length; i++)
            {
                var requiredProperty = propertiesWithParameterAttr.SingleOrDefault(
                                            p => p.GetCustomAttributes(false)
                                                  .OfType<ParameterIndexAttribute>()
                                                  .Any(a => a.Index == i));
                if (requiredProperty == null)
                    throw new Exception("Unable to find a mapping parameter with index " + i);
                requiredProperty.SetValue(instance, args[i]);
            }
            return instance;
        }
    }
}

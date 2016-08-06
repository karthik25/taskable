using System;
using TaskableRoslyn.Contracts;
using TaskableRoslynCore.TaskLoader;

namespace TaskableRoslynCore
{
    public static class SourceFileProviderFactory
    {
        public static ISourceFileSyntaxProvider CreateSourceProvider(LanguageType type)
        {
            if (type == LanguageType.CSharp)
                return new CSharpSourceFileSyntaxProvider();
            throw new NotImplementedException("VB Source File Provider has not been implemented.");
        }
    }
}

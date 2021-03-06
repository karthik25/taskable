﻿using System;
using ColoredConsole;

namespace TaskableCore.Extensions
{
    public static class ConsoleExtensions
    {
        public static void Print(this string src)
        {
            Console.Write(src);
        }

        public static void PrintDefault(this string src, params object[] args)
        {
            Console.WriteLine(src, args);
        }

        public static void PrintGreen(this string src, params object[] args)
        {
            ColorConsole.WriteLine(string.Format(src, args).Green());
        }

        public static void PrintBlue(this string src, params object[] args)
        {
            ColorConsole.WriteLine(string.Format(src, args).Blue());
        }

        public static void PrintRed(this string src, params object[] args)
        {
            ColorConsole.WriteLine(string.Format(src, args).Red());
        }

        public static void PrintYellow(this string src, params object[] args)
        {
            ColorConsole.WriteLine(string.Format(src, args).Yellow());
        }
    }
}

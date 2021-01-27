// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TrickyBot.API.Features
{
    public static class Log
    {
        public static void Debug(object message) => SendPrivate(message, LogLevel.Debug, ConsoleColor.Green);

        public static void Info(object message) => SendPrivate(message, LogLevel.Info, ConsoleColor.Cyan);

        public static void Warn(object message) => SendPrivate(message, LogLevel.Warn, ConsoleColor.Magenta);

        public static void Error(object message) => SendPrivate(message, LogLevel.Error, ConsoleColor.Red);

        public static void Send(object message, LogLevel logLevel, ConsoleColor color) => SendPrivate(message, logLevel, color);

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void SendPrivate(object message, LogLevel logLevel, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            var stackTrace = new StackTrace();
            var callingAssembly = stackTrace.GetFrame(2).GetMethod().DeclaringType.Assembly;
            var assemblyName = callingAssembly.GetName().Name;
            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{logLevel.ToString().ToUpper()}] [{assemblyName}] {message}");
            Console.ForegroundColor = previousColor;
        }
    }
}
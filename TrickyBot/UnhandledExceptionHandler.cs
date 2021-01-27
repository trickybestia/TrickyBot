// -----------------------------------------------------------------------
// <copyright file="UnhandledExceptionHandler.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

using TrickyBot.API.Features;

namespace TrickyBot
{
    internal static class UnhandledExceptionHandler
    {
        static UnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        public static void Init()
        {
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error($"Unhandled exception (terminating: {e.IsTerminating}):\n{e.ExceptionObject}");
        }
    }
}
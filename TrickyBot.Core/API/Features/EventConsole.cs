// -----------------------------------------------------------------------
// <copyright file="EventConsole.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace TrickyBot.API.Features
{
    internal static class EventConsole
    {
        static EventConsole()
        {
            Task.Run(ReadLineCycle);
        }

        public static event Action<string> OnReadLine;

        private static void ReadLineCycle()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (OnReadLine is not null)
                {
                    OnReadLine(input);
                }
            }
        }
    }
}
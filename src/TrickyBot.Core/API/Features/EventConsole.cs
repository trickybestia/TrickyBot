// -----------------------------------------------------------------------
// <copyright file="EventConsole.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Обёртка над консолью, предоставляющая событийный интерфейс для работы с ней.
    /// </summary>
    internal static class EventConsole
    {
        static EventConsole()
        {
            new Task(ReadLineCycle, TaskCreationOptions.LongRunning).Start();
        }

        /// <summary>
        /// Событие чтения строки из консоли.
        /// </summary>
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
// -----------------------------------------------------------------------
// <copyright file="ConsoleHelper.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

internal static class ConsoleHelper
{
    static ConsoleHelper()
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
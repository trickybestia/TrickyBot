// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;

using TrickyBot.API.Features;

namespace TrickyBot
{
    internal static class Program
    {
        private static async Task Main()
        {
            UnhandledExceptionHandler.Init();
            Paths.BotCore = Directory.GetCurrentDirectory();
            Paths.Data = Path.Combine(Paths.BotCore, "../TrickyBotData");
            Paths.Init();
            var token = Environment.GetEnvironmentVariable("BotToken");
            Bot bot = new Bot();
            await bot.Start(token);
            Log.Info("Type \"exit\" to stop bot.");
            await bot.WaitToStopAsync();
        }
    }
}
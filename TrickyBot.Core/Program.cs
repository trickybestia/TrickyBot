// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

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
            var dataDirectory = CommandLineArgsParser.Args["data"];
            if (Path.IsPathFullyQualified(dataDirectory))
            {
                Paths.Data = dataDirectory;
            }
            else
            {
                Paths.Data = Path.Combine(Paths.BotCore, dataDirectory);
            }

            Paths.Init();
            var token = TokenProvider.GetToken();
            Bot bot = new Bot();
            await bot.StartAsync(token);
            Log.Info("Type \"exit\" to stop bot.");
            await bot.WaitToStopAsync();
        }
    }
}
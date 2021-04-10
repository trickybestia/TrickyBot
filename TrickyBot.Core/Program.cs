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
    /// <summary>
    /// Класс, содержащий главную точку входа в приложение.
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            UnhandledExceptionHandler.Init();
            Paths.BotCore = Directory.GetCurrentDirectory();
            if (CommandLineArgsParser.Args.TryGetValue("data", out string dataDirectory))
            {
                if (Path.IsPathFullyQualified(dataDirectory))
                {
                    Paths.Data = dataDirectory;
                }
                else
                {
                    Paths.Data = Path.Combine(Paths.BotCore, dataDirectory);
                }
            }
            else
            {
                Paths.Data = Path.Combine(Paths.BotCore, "Data");
            }

            Paths.Init();
            await ServiceManager.StartAsync();
            Log.Info(typeof(Program), "Введите \"exit\", чтобы остановить бота и выйти.");
            await ServiceManager.WaitToStopAsync();
        }
    }
}
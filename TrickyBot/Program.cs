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
            Paths.ServicesRoot = Path.Combine(Paths.BotCore, "../TrickyBotData");
            var token = Environment.GetEnvironmentVariable("BotToken");
            Bot bot = new Bot();
            await bot.Start(token);
            Log.Info("Type 'stop' to stop.");
            while (Console.ReadLine() != "stop")
            { }
            Log.Info("Stopping...");
            await bot.Stop();
        }
    }
}
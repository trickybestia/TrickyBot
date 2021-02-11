// -----------------------------------------------------------------------
// <copyright file="Exit.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService.ConsoleCommands
{
    internal class Exit : IConsoleCommand
    {
        public string Name { get; } = "exit";

        public ConsoleCommandRunMode RunMode => ConsoleCommandRunMode.Sync;

        public async Task ExecuteAsync(string parameter)
        {
            await Bot.Instance.StopAsync();
        }
    }
}
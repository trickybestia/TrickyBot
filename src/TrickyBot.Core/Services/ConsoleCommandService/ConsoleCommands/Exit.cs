// -----------------------------------------------------------------------
// <copyright file="Exit.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService.ConsoleCommands
{
    /// <summary>
    /// Команда завершения работы бота.
    /// </summary>
    internal class Exit : IConsoleCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "exit";

        /// <inheritdoc/>
        public ConsoleCommandRunMode RunMode => ConsoleCommandRunMode.Sync;

        /// <inheritdoc/>
        public async Task ExecuteAsync(string parameter)
        {
            await ServiceManager.StopAsync();
        }
    }
}
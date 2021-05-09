// -----------------------------------------------------------------------
// <copyright file="Services.cs" company="The TrickyBot Team">
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
    /// Команда вывода списка загруженных сервисов.
    /// </summary>
    internal class Services : IConsoleCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "services";

        /// <inheritdoc/>
        public ConsoleCommandRunMode RunMode => ConsoleCommandRunMode.Sync;

        /// <inheritdoc/>
        public Task ExecuteAsync(string parameter)
        {
            foreach (var service in ServiceManager.Services)
            {
                Log.Info(this, $"{service.Info.Name} v{service.Info.Version} ({service.Info.GithubRepositoryUrl}) от \"{service.Info.Author}\":{service.State}:{service.Priority.Value}");
            }

            return Task.CompletedTask;
        }
    }
}
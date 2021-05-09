// -----------------------------------------------------------------------
// <copyright file="PatternMatchingService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.PatternMatchingService.ConsoleCommands;

namespace TrickyBot.Services.PatternMatchingService
{
    /// <summary>
    /// Сервис для синтаксического разбора параметров команд.
    /// </summary>
    public class PatternMatchingService : ServiceBase<PatternMatchingServiceConfig>, IConsoleCommandService<PatternMatchingServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = new IConsoleCommand[]
        {
            new Analyze(),
            new Parse(),
        };

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(PatternMatchingService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <inheritdoc/>
        protected override Task OnStart() => Task.CompletedTask;

        /// <inheritdoc/>
        protected override Task OnStop() => Task.CompletedTask;
    }
}